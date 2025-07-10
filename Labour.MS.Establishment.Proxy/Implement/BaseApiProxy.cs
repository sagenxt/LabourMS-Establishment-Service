using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Labour.MS.Establishment.Models.Proxy.Common;
using Labour.MS.Establishment.Proxy.Interface;
using Labour.MS.Establishment.Proxy.Interface.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Labour.MS.Establishment.Proxy.Implement
{
    public class BaseApiProxy : IBaseApiProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseApiProxy> _logger;
        private readonly IOptions<ApiProxyOptions> _options;
        private readonly JsonSerializerOptions _jsonOptions;

        public BaseApiProxy(ILogger<BaseApiProxy> logger, IOptions<ApiProxyOptions> options, HttpClient httpClient)
        {
            _logger = logger;
            _options = options;
            _httpClient = httpClient;
            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            ConfigureHttpClient();
        }

        public async Task<ApiProxyResponse<T>> InvokeApi<T>(HttpMethod httpMethod, string endPoint, IRequestHeaders requestHeaders)
        {
            var request = new HttpRequestMessage(httpMethod, endPoint);
            return await InvokeApi<T>(request, requestHeaders.CorrelationId);
        }

        public async Task<ApiProxyResponse<T>> InvokeApi<Q, T>(HttpMethod httpMethod, string endPoint, Q request, IRequestHeaders requestHeaders)
        {
            using var httpRequest = BuildHttpRequestAsync(httpMethod, endPoint, request, requestHeaders);
            return await InvokeApi<T>(httpRequest, requestHeaders.CorrelationId);
        }

        private async Task<ApiProxyResponse<T>> InvokeApi<T>(HttpRequestMessage httpRequest, string? requestId, bool hasMultipleErrors = false)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                if (_options.Value.EnableLogging)
                {
                    LogRequest(requestId, httpRequest);
                }

                using var response = await _httpClient.SendAsync(httpRequest);
                stopwatch.Stop();

                if (_options.Value.EnableLogging)
                {
                    LogResponse(requestId, response, stopwatch.Elapsed);
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = string.IsNullOrEmpty(responseContent)
                        ? default(T)
                        : JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);

                    return ApiProxyResponse<T>.Success(result!, response.StatusCode, stopwatch.Elapsed);
                }
                else
                {
                    var errorMessage = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        errorMessage += $": {responseContent}";
                    }

                    return ApiProxyResponse<T>.Failure(errorMessage, response.StatusCode, stopwatch.Elapsed);
                }
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Request {RequestId} timed out after {Elapsed}ms", requestId, stopwatch.ElapsedMilliseconds);
                return ApiProxyResponse<T>.Failure("Request timed out", HttpStatusCode.RequestTimeout, stopwatch.Elapsed);
            }
            catch (HttpRequestException ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "HTTP request error for {RequestId}: {Message}", requestId, ex.Message);
                return ApiProxyResponse<T>.Failure($"HTTP request error: {ex.Message}", HttpStatusCode.ServiceUnavailable, stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Unexpected error for request {RequestId}: {Message}", requestId, ex.Message);
                return ApiProxyResponse<T>.Failure($"Unexpected error: {ex.Message}", HttpStatusCode.InternalServerError, stopwatch.Elapsed);
            }
        }

        private HttpRequestMessage BuildHttpRequestAsync<Q>(HttpMethod method, string endpoint, Q? request, IRequestHeaders requestHeaders)
        {
            var httpRequest = new HttpRequestMessage(method, endpoint);
            // Add custom headers
            if (requestHeaders != null)
            {
                requestHeaders.AddToHeaders(httpRequest.Headers);
            }

            // Add request body for POST, PUT, PATCH
            if (request != null && (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch))
            {
                var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
                httpRequest.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            return httpRequest;
        }

        private void ConfigureHttpClient()
        {
            if (_httpClient.BaseAddress ==  null && !string.IsNullOrEmpty(_options.Value.BaseUrl))
            {
                _httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
                _httpClient.Timeout = _options.Value.Timeout;
            }

            // Add default headers
            foreach (var header in _options.Value.DefaultHeaders)
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Configure authentication
            ConfigureAuthentication();
        }

        private void ConfigureAuthentication()
        {
            var auth = _options.Value.Authentication;

            switch (auth.Type.ToLower())
            {
                case "bearer":
                    if (!string.IsNullOrEmpty(auth.Token))
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token);
                    }
                    break;

                case "basic":
                    if (!string.IsNullOrEmpty(auth.Username) && !string.IsNullOrEmpty(auth.Password))
                    {
                        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{auth.Username}:{auth.Password}"));
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                    }
                    break;

                case "apikey":
                    if (!string.IsNullOrEmpty(auth.Token))
                    {
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(auth.ApiKeyHeaderName, auth.Token);
                    }
                    break;
            }
        }

        private void LogRequest(string? requestId, HttpRequestMessage request)
        {
            if (!_options.Value.EnableLogging) return;

            var logMessage = $"Request {requestId}: {request.Method} {request.RequestUri}";

            if (_options.Value.LogSensitiveData)
            {
                logMessage += $"\nHeaders: {string.Join(", ", request.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}"))}";

                if (request.Content != null)
                {
                    var content = request.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(content))
                    {
                        logMessage += $"\nBody: {content}";
                    }
                }
            }

            _logger.LogInformation(logMessage);
        }

        private void LogResponse(string? requestId, HttpResponseMessage response, TimeSpan elapsed)
        {
            if (!_options.Value.EnableLogging) return;

            var logMessage = $"Response {requestId}: {(int)response.StatusCode} {response.ReasonPhrase} in {elapsed.TotalMilliseconds}ms";

            if (_options.Value.LogSensitiveData)
            {
                logMessage += $"\nHeaders: {string.Join(", ", response.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}"))}";
            }

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(logMessage);
            }
            else
            {
                _logger.LogWarning(logMessage);
            }
        }


    }
}
