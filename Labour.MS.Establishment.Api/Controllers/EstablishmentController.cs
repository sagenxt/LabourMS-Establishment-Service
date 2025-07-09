using Core.ApiResponse.Interface;
using Labour.MS.Establishment.Api.Controllers.BaseController;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Models.DTOs.Response;
using Labour.MS.Establishment.Proxy.RequestHeaders;
using Labour.MS.Establishment.Service.Interface;
using Labour.MS.Establishment.Utility.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Labour.MS.Establishment.Api.Controllers
{
    public class EstablishmentController : BaseApiController
    {
        private readonly IApiResponseFactory _apiResponseFactory;
        private readonly IEstablishmentService _establishmentService;

        public EstablishmentController(IApiResponseFactory apiResponseFactory,
                                        IEstablishmentService establishmentService)
        {
            this._apiResponseFactory = apiResponseFactory;
            this._establishmentService = establishmentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IApiResponse<EstablishmentDetailsResponse>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok", typeof(IApiResponse<EstablishmentDetailsResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Authentication Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authorisation Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Service Unavailable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status499ClientClosedRequest, "Client Closed Request")]
        [Route(ApiInfoConstant.EstablishmentAllDetails)]
        public async Task<IActionResult> RetrieveAllEstablishmentDetails(
            [FromHeader] EstablishmentRequestHeaders requestHeaders)
        {
            return this._apiResponseFactory.CreateResponse(await this._establishmentService.RetrieveAllEstablishmentDetailsAsync(requestHeaders));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IApiResponse<EstablishmentResponse>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok", typeof(IApiResponse<EstablishmentResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Authentication Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authorisation Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Service Unavailable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status499ClientClosedRequest, "Client Closed Request")]
        [Route(ApiInfoConstant.Establishment)]
        public async Task<IActionResult> PersistEstablishmentDetails(
            [FromHeader] EstablishmentRequestHeaders requestHeaders,
            [FromBody] EstablishmentDetailsRequest establishmentRequest)
        {
            return this._apiResponseFactory.CreateResponse(await this._establishmentService.PersistEstablishmentInfoAsync(establishmentRequest, requestHeaders));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IApiResponse<EstablishmentDetailsResponse>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok", typeof(IApiResponse<EstablishmentDetailsResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Authentication Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authorisation Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "Service Unavailable", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error", typeof(string))]
        [SwaggerResponse(StatusCodes.Status499ClientClosedRequest, "Client Closed Request")]
        [Route(ApiInfoConstant.EstablishmentDetails)]
        public async Task<IActionResult> RetrieveEstablishmentDetails(
            [FromHeader] EstablishmentRequestHeaders requestHeaders,
            [FromBody] EstablishmentRequest request)
        {
            return this._apiResponseFactory.CreateResponse(await this._establishmentService.RetrieveEstablishmentDetailsAsync(request, requestHeaders));
        }
    }
}
