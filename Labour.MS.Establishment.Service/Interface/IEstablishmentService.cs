using Core.ApiResponse.Interface;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Models.DTOs.Response;
using Labour.MS.Establishment.Proxy.RequestHeaders;

namespace Labour.MS.Establishment.Service.Interface
{
    public interface IEstablishmentService
    {
        Task<IApiResponse<IEnumerable<EstablishmentDetailsResponse?>>> RetrieveAllEstablishmentDetailsAsync(EstablishmentRequestHeaders requestHeaders);
        Task<IApiResponse<EstablishmentDetailsResponse?>> RetrieveEstablishmentDetailsAsync(EstablishmentRequest request, EstablishmentRequestHeaders requestHeaders);
        Task<IApiResponse<EstablishmentResponse?>> PersistEstablishmentInfoAsync(EstablishmentDetailsRequest request, EstablishmentRequestHeaders requestHeaders);
    }
}
