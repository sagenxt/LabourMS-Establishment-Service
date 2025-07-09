using Core.ApiResponse.Implementation;
using Core.ApiResponse.Interface;
using FluentValidation;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Models.Proxy.Common;
using Labour.MS.Establishment.Proxy.Implement;
using Labour.MS.Establishment.Proxy.Interface;
using Labour.MS.Establishment.Service.Implement;
using Labour.MS.Establishment.Service.Interface;
using Labour.MS.Establishment.Service.Validators;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace Labour.MS.Establishment.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add IApiResponseFactory to the container
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITraceProvider, TraceProvider>();
            services.AddTransient<IApiResponseFactory, ApiResponseFactory>();

            services.AddScoped<IBaseApiProxy, BaseApiProxy>();

            services.AddScoped<IHttpStatusCodeResolver, HttpStatusCodeResolver>();


            // Add Proxy services to the container.
            services.AddScoped<IServiceApiProxy, ServiceApiProxy>();

            // Add Services to the container.
            services.AddScoped<IEstablishmentService, EstablishmentService>();

            // Add Validators to the container.
            services.AddScoped<IValidator<EstablishmentDetailsRequest>, EstablishmentRequestDetailsValidator>();
            services.AddScoped<IValidator<EstablishmentRequest>, EstablishmentRequestValidator>();

            services.AddBaseApiProxy(configuration.GetSection(ApiProxyOptions.SectionName));

            return services;
        }

        /// <summary>
        /// Adds BaseApiProxy services to the service collection with configuration section
        /// </summary>
        public static IServiceCollection AddBaseApiProxy(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<ApiProxyOptions>(configurationSection);

            var options = new ApiProxyOptions();
            configurationSection.Bind(options);

            return services.AddBaseApiProxy(options);
        }

        /// <summary>
        /// Adds BaseApiProxy services to the service collection with options
        /// </summary>
        public static IServiceCollection AddBaseApiProxy(this IServiceCollection services, ApiProxyOptions options)
        {
            services.Configure<ApiProxyOptions>(opts =>
            {
                opts.BaseUrl = options.BaseUrl;
                opts.Timeout = options.Timeout;
                opts.MaxRetryAttempts = options.MaxRetryAttempts;
                opts.RetryDelayMs = options.RetryDelayMs;
                opts.UseExponentialBackoff = options.UseExponentialBackoff;
                opts.DefaultHeaders = options.DefaultHeaders;
                opts.Authentication = options.Authentication;
                opts.EnableLogging = options.EnableLogging;
                opts.LogSensitiveData = options.LogSensitiveData;
            });

            return services.AddBaseApiProxy();
        }

        /// <summary>
        /// Adds BaseApiProxy services to the service collection with action configuration
        /// </summary>
        public static IServiceCollection AddBaseApiProxy(this IServiceCollection services, Action<ApiProxyOptions> configureOptions)
        {
            services.Configure(configureOptions);
            return services.AddBaseApiProxy();
        }

        /// <summary>
        /// Adds BaseApiProxy services to the service collection
        /// </summary>
        private static IServiceCollection AddBaseApiProxy(this IServiceCollection services)
        {
            services.AddHttpClient<IBaseApiProxy, BaseApiProxy>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<ApiProxyOptions>>().Value;

                if (!string.IsNullOrEmpty(options.BaseUrl))
                {
                    client.BaseAddress = new Uri(options.BaseUrl);
                }

                client.Timeout = options.Timeout;
            })
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var options = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<ApiProxyOptions>>().Value;
                var logger = serviceProvider.GetRequiredService<ILogger<BaseApiProxy>>();

                return CreateRetryPolicy(options, logger);
            });

            return services;
        }

        /// <summary>
        /// Adds a named BaseApiProxy client to the service collection
        /// </summary>
        public static IServiceCollection AddBaseApiProxy(this IServiceCollection services, string name, IConfiguration configuration)
        {
            return services.AddBaseApiProxy(name, configuration.GetSection($"{ApiProxyOptions.SectionName}:{name}"));
        }

        /// <summary>
        /// Adds a named BaseApiProxy client to the service collection with configuration section
        /// </summary>
        public static IServiceCollection AddBaseApiProxy(this IServiceCollection services, string name, IConfigurationSection configurationSection)
        {
            var options = new ApiProxyOptions();
            configurationSection.Bind(options);

            return services.AddBaseApiProxy(name, options);
        }

        /// <summary>
        /// Adds a named BaseApiProxy client to the service collection with options
        /// </summary>
        public static IServiceCollection AddBaseApiProxy(this IServiceCollection services, string name, ApiProxyOptions options)
        {
            services.AddHttpClient(name, client =>
            {
                if (!string.IsNullOrEmpty(options.BaseUrl))
                {
                    client.BaseAddress = new Uri(options.BaseUrl);
                }

                client.Timeout = options.Timeout;

                // Add default headers
                foreach (var header in options.DefaultHeaders)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            })
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<BaseApiProxy>>();
                return CreateRetryPolicy(options, logger);
            });

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy(ApiProxyOptions options, ILogger logger)
        {
            if (options.MaxRetryAttempts <= 0)
            {
                return Policy.NoOpAsync<HttpResponseMessage>();
            }

            var delay = options.UseExponentialBackoff
                ? Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromMilliseconds(options.RetryDelayMs), options.MaxRetryAttempts)
                : Enumerable.Range(0, options.MaxRetryAttempts).Select(_ => TimeSpan.FromMilliseconds(options.RetryDelayMs));

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    delay,
                    onRetry: (outcome, timespan, retryCount, context) =>
                    {
                        logger.LogWarning("Retry attempt {RetryCount} for {RequestUri} after {Delay}ms. Reason: {Reason}",
                            retryCount,
                            context.GetValueOrDefault("RequestUri", "Unknown"),
                            timespan.TotalMilliseconds,
                            outcome.Exception?.Message ?? outcome.Result?.ReasonPhrase ?? "Unknown");
                    });
        }

    }
}
