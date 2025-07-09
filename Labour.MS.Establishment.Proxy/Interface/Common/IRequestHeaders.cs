using System.Net.Http.Headers;

namespace Labour.MS.Establishment.Proxy.Interface.Common
{
    public interface IRequestHeaders
    {
        /// <summary>
        /// Unique string for the request.
        /// </summary>
        string? XRequestId { get; set; }
        /// <summary>
        /// Unique string used for the tracking purpose.
        /// </summary>
        string? CorrelationId { get; set; }

        /// <summary>
        /// Adds the request headers to the provided HttpRequestHeaders object.
        /// </summary>
        /// <param name="requestHeaders"></param>
        void AddToHeaders(HttpRequestHeaders requestHeaders);
    }
}
