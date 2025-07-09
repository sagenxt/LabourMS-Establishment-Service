using Labour.MS.Establishment.Models.Proxy.Common;
using Labour.MS.Establishment.Models.Proxy.Request;
using Labour.MS.Establishment.Models.Proxy.Response;
using Labour.MS.Establishment.Proxy.RequestHeaders;

namespace Labour.MS.Establishment.Proxy.Interface
{
    public interface IServiceApiProxy
    {
        Task<ApiProxyResponse<EstablishmentAdapterResponse>> PersistEstablishmentDetailsAsync(EstablishmentDetailsAdapterRequest request, EstablishmentRequestHeaders requestHeaders);
        Task<ApiProxyResponse<EstablishmentDetailsAdapterResponse>> GetAllEstablishmentDetailsAsync(EstablishmentRequestHeaders requestHeaders);
        Task<ApiProxyResponse<EstablishmentDetailsAdapterResponse>> GetEstablishmentDetailsAsync(EstablishmentAdapterRequest request, EstablishmentRequestHeaders requestHeaders);
    }

}
