using System.Net.Http.Headers;
using System.Reflection;
using Labour.MS.Establishment.Proxy.Interface.Common;
using Labour.MS.Establishment.Utility.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Labour.MS.Establishment.Proxy
{
    public class BaseRequestHeaders : IRequestHeaders
    {
        /// <summary>
        /// Unique guid for the request.
        /// </summary>
        [FromHeader(Name = ApiInfoConstant.X_REQUEST_ID)]
        public string? XRequestId { get; set; }

        /// <summary>
        /// unique guid used for the tracking purpose.
        /// </summary>
        [FromHeader(Name = ApiInfoConstant.CORRELATION_ID)]
        public string? CorrelationId { get; set; }

        public void AddToHeaders(HttpRequestHeaders requestHeaders)
        {
            foreach (var property in this.GetType().GetProperties())
            {
                var fromHeaderAttribute = property.GetCustomAttribute<FromHeaderAttribute>();
                if (fromHeaderAttribute != null)
                {
                    var headerName = fromHeaderAttribute.Name ?? property.Name;
                    if (headerName == HeaderNames.Authorization)
                    {
                        // Set the Authorization header differently.
                        var headerValue = property.GetValue(this);
                        if (headerValue != null)
                        {
                            requestHeaders.Authorization = (AuthenticationHeaderValue)headerValue;
                        }
                    }
                    else
                    {
                        var headerValue = property.GetValue(this)?.ToString();
                        if (headerValue != null)
                        {
                            requestHeaders.Add(headerName, headerValue);
                        }
                    }
                }
            }
        }
    }
}
