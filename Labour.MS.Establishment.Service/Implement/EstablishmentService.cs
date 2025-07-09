using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ApiResponse.Interface;
using Microsoft.Extensions.Logging;
using FluentValidation;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Service.Interface;
using AutoMapper;
using Labour.MS.Establishment.Proxy.Interface;
using Labour.MS.Establishment.Models.DTOs.Response;
using Labour.MS.Establishment.Proxy.RequestHeaders;
using Labour.MS.Establishment.Utility.Constants;
using Microsoft.AspNetCore.Http.Headers;
using Org.BouncyCastle.Asn1.Ocsp;
using Labour.MS.Establishment.Models.Proxy.Request;
using Labour.MS.Establishment.Service.Mappers;

namespace Labour.MS.Establishment.Service.Implement
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IApiResponseFactory _apiResponseFactory;
        private readonly IValidator<EstablishmentDetailsRequest> _establishmentRequestDetailValidator;
        private readonly IValidator<EstablishmentRequest> _establishmentRequestValidator;
        private readonly IServiceApiProxy _serviceApiProxy;
        public EstablishmentService(ILoggerFactory loggerFactory,
                            IMapper mapper,
                            IApiResponseFactory apiResponseFactory,
                            IValidator<EstablishmentDetailsRequest> establishmentRequestDetailValidator,
                            IValidator<EstablishmentRequest> establishmentRequestValidator,
                            IServiceApiProxy serviceApiProxy)
        {
            this._logger = loggerFactory.CreateLogger<EstablishmentService>();
            this._mapper = mapper;
            this._apiResponseFactory = apiResponseFactory;
            this._establishmentRequestDetailValidator = establishmentRequestDetailValidator;
            this._establishmentRequestValidator = establishmentRequestValidator;
            this._serviceApiProxy = serviceApiProxy;
        }

        public async Task<IApiResponse<IEnumerable<EstablishmentDetailsResponse?>>> RetrieveAllEstablishmentDetailsAsync(EstablishmentRequestHeaders requestHeaders)
        {
            this._logger.LogInformation($"Method Name : {nameof(RetrieveAllEstablishmentDetailsAsync)} started");
            try
            {
                var response = await this._serviceApiProxy.GetAllEstablishmentDetailsAsync(requestHeaders);

                if (!response.IsSuccessStatusCode)
                {
                    this._logger.LogWarning("Error occurred while retrieving establishment details.");
                    return this._apiResponseFactory.BadRequestApiResponse<IEnumerable<EstablishmentDetailsResponse?>>("" ?? "Unknown error", nameof(RetrieveAllEstablishmentDetailsAsync));
                }

                var establishmentDetails = this._mapper.Map<IEnumerable<EstablishmentDetailsResponse?>>(response.Data);

                this._logger.LogInformation($"Method Name : {nameof(RetrieveAllEstablishmentDetailsAsync)} completed");
                return this._apiResponseFactory.ValidApiResponse(establishmentDetails);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"An exception occurred while retrieving establishment details");
                return this._apiResponseFactory.InternalServerErrorApiResponse<IEnumerable<EstablishmentDetailsResponse?>>(
                    "An unexpected error occurred while processing the request and response.",
                    nameof(RetrieveAllEstablishmentDetailsAsync));
            }
        }

        public async Task<IApiResponse<EstablishmentDetailsResponse?>> RetrieveEstablishmentDetailsAsync(EstablishmentRequest request, EstablishmentRequestHeaders requestHeaders)
        {
            this._logger.LogInformation($"Method Name : {nameof(RetrieveEstablishmentDetailsAsync)} started");
            try
            {
                var validationResult = await this._establishmentRequestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    string errorMessage = string.Empty;
                    foreach (var error in validationResult.Errors)
                    {
                        errorMessage = !string.IsNullOrEmpty(errorMessage) ? errorMessage + ", " + error.ErrorMessage : error.ErrorMessage;
                    }
                    this._logger.LogWarning(string.Format(WarningMessages.InvalidRequestForEstablishmentDetails, errorMessage));
                    return this._apiResponseFactory.BadRequestApiResponse<EstablishmentDetailsResponse?>(string.Format(WarningMessages.InvalidRequestForEstablishmentDetails, errorMessage), nameof(RetrieveEstablishmentDetailsAsync));
                }

                var establishmentRequest = this._mapper.Map<EstablishmentAdapterRequest?>(request);
                var response = await this._serviceApiProxy.GetEstablishmentDetailsAsync(establishmentRequest, requestHeaders);

                if (!response.IsSuccessStatusCode)
                {
                    this._logger.LogWarning("Error occurred while retrieving establishment details.");
                    return this._apiResponseFactory.BadRequestApiResponse<EstablishmentDetailsResponse?>("" ?? "Unknown error", nameof(RetrieveEstablishmentDetailsAsync));
                }

                if (response.Data == null || response.Data.Data == null)
                {
                    this._logger.LogWarning("No establishment details found for the provided establishment id.");
                    return this._apiResponseFactory.NotFoundApiResponse<EstablishmentDetailsResponse?>("No establishment details found for the provided establishment id.", nameof(RetrieveEstablishmentDetailsAsync));
                }

                var establishmentDetails = EstablishmentResponseMapper.MapToEstalishmentDetailsResponse(response.Data.Data);

                this._logger.LogInformation($"Method Name : {nameof(RetrieveEstablishmentDetailsAsync)} completed");
                return this._apiResponseFactory.ValidApiResponse(establishmentDetails);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"An exception occurred while retrieving establishment details based on establishment id: {request.EstablishmentId}");
                return this._apiResponseFactory.InternalServerErrorApiResponse<EstablishmentDetailsResponse?>(
                    "An unexpected error occurred while processing the request and response.",
                    nameof(RetrieveEstablishmentDetailsAsync));
            }
        }

        public async Task<IApiResponse<EstablishmentResponse?>> PersistEstablishmentInfoAsync(EstablishmentDetailsRequest request, EstablishmentRequestHeaders requestHeaders)
        {
            this._logger.LogInformation($"Method Name : {nameof(PersistEstablishmentInfoAsync)} started");
            try
            {
                if (request == null)
                {
                    this._logger.LogWarning("Request object is null.");
                    return this._apiResponseFactory.BadRequestApiResponse<EstablishmentResponse?>("Request object cannot be null.", nameof(PersistEstablishmentInfoAsync));
                }

                var validationResult = await this._establishmentRequestDetailValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    string errorMessage = string.Empty;
                    foreach (var error in validationResult.Errors)
                    {
                        errorMessage = !string.IsNullOrEmpty(errorMessage) ? errorMessage + ", " + error.ErrorMessage : error.ErrorMessage;
                    }
                    this._logger.LogWarning(string.Format(WarningMessages.InvalidEstablishmentRequestDetails, errorMessage));
                    return this._apiResponseFactory.BadRequestApiResponse<EstablishmentResponse?>(string.Format(WarningMessages.InvalidEstablishmentRequestDetails, errorMessage), nameof(PersistEstablishmentInfoAsync));
                }

                var establishmentRequest = this._mapper.Map<EstablishmentDetailsAdapterRequest?>(request);
                if (establishmentRequest == null)
                {
                    this._logger.LogWarning("Mapping resulted in a null EstablishmentDetailsAdapterRequest.");
                    return this._apiResponseFactory.BadRequestApiResponse<EstablishmentResponse?>("Mapping error: EstablishmentDetailsAdapterRequest is null.", nameof(PersistEstablishmentInfoAsync));
                }

                var response = await this._serviceApiProxy.PersistEstablishmentDetailsAsync(establishmentRequest, requestHeaders);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.Data != null && response.Data.StatusCode != 200)
                    {
                        this._logger.LogWarning(response.Data.Message);
                        return this._apiResponseFactory.BadRequestApiResponse<EstablishmentResponse?>(response.Data.Message ?? "Unknown error", nameof(PersistEstablishmentInfoAsync));
                    }

                    var establishmentResponse = this._mapper.Map<EstablishmentResponse?>(response.Data);
                    this._logger.LogInformation($"Method Name : {nameof(PersistEstablishmentInfoAsync)} completed");
                    return this._apiResponseFactory.ValidApiResponse(establishmentResponse);
                }
                else
                {
                    this._logger.LogWarning("Error occurred while saving establishment info.");
                    return this._apiResponseFactory.BadRequestApiResponse<EstablishmentResponse?>("" ?? "Unknown error", nameof(PersistEstablishmentInfoAsync));
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "An exception occurred in persisting establishment information.");
                return this._apiResponseFactory.InternalServerErrorApiResponse<EstablishmentResponse?>(
                    "An unexpected error occurred while processing the request.",
                    nameof(PersistEstablishmentInfoAsync));
            }
        }

    }
}
