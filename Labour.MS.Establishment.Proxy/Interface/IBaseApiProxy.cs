using Labour.MS.Establishment.Models.Proxy.Common;
using Labour.MS.Establishment.Proxy.Interface.Common;

namespace Labour.MS.Establishment.Proxy.Interface
{
    public interface IBaseApiProxy
    {
        Task<ApiProxyResponse<T>> InvokeApi<T>(HttpMethod httpMethod, string endPoint, IRequestHeaders requestHeaders);
        Task<ApiProxyResponse<T>> InvokeApi<Q, T>(HttpMethod httpMethod, string endPoint, Q request, IRequestHeaders requestHeaders);
    }
}
