using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labour.MS.Establishment.Models.Proxy.Common;
using Labour.MS.Establishment.Models.Proxy.Request;
using Labour.MS.Establishment.Models.Proxy.Response;
using Labour.MS.Establishment.Proxy.Interface;
using Labour.MS.Establishment.Proxy.RequestHeaders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Labour.MS.Establishment.Proxy.Implement
{
    public class ServiceApiProxy(
            ILogger<ServiceApiProxy> logger,
            IOptions<ApiProxyOptions> options,
            HttpClient httpClient) : BaseApiProxy(logger, options, httpClient), IServiceApiProxy
    {
        //private readonly ILogger<ServiceApiProxy> _logger;
        //private readonly IOptions<ApiConfigModel> _apiProxyConfig;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IOptions<IDPTokenOption> _idpTokenOptions;
        //private readonly IIDPTokenService _idpTokenService;
        //private readonly HttpClient _httpClient;

        //public ServiceApiProxy(
        //    ILogger<ServiceApiProxy> logger,
        //    IOptions<ApiConfigModel> apiProxyConfig,
        //    IHttpContextAccessor httpContextAccessor,
        //    IOptions<IDPTokenOption> idpTokenOptions,
        //    IIDPTokenService idpTokenService,
        //    HttpClient httpClient)
        //    : BaseApiProxy(logger, apiProxyConfig, httpContextAccessor, idpTokenOptions, idpTokenService, httpClient), IServiceApiProxy
        //{
        //    _logger = logger;
        //    _apiProxyConfig = apiProxyConfig;
        //    _httpContextAccessor = httpContextAccessor;
        //    _idpTokenOptions = idpTokenOptions;
        //    _idpTokenService = idpTokenService;
        //    _httpClient = httpClient;
        //}

        public async Task<ApiProxyResponse<EstablishmentDetailsAdapterResponse>> GetAllEstablishmentDetailsAsync(EstablishmentRequestHeaders requestHeaders)
        {
            return await InvokeApi<EstablishmentDetailsAdapterResponse>(
                httpMethod: HttpMethod.Get,
                endPoint: "api/Establishment/GetAllEstablishmentDetails",
                requestHeaders: requestHeaders);
        }

        public async Task<ApiProxyResponse<EstablishmentDetailsAdapterResponse>> GetEstablishmentDetailsAsync(EstablishmentAdapterRequest request, EstablishmentRequestHeaders requestHeaders)
        {
            return await InvokeApi<EstablishmentAdapterRequest, EstablishmentDetailsAdapterResponse>(
                httpMethod: HttpMethod.Post,
                endPoint: "adapter/establishment/details",
                request: request,
                requestHeaders: requestHeaders);
        }

        public async Task<ApiProxyResponse<EstablishmentAdapterResponse>> PersistEstablishmentDetailsAsync(EstablishmentDetailsAdapterRequest request, EstablishmentRequestHeaders requestHeaders)
        {
            return await InvokeApi<EstablishmentDetailsAdapterRequest, EstablishmentAdapterResponse>(
                httpMethod: HttpMethod.Post,
                endPoint: "api/Establishment/PersistEstablishmentDetails",
                request: request,
                requestHeaders: requestHeaders);
        }


    }
}
