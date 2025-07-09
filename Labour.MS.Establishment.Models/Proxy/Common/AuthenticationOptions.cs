using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labour.MS.Establishment.Models.Proxy.Common
{
    /// <summary>
    /// Authentication configuration options
    /// </summary>
    public class AuthenticationOptions
    {
        /// <summary>
        /// Type of authentication (Bearer, Basic, ApiKey)
        /// </summary>
        public string Type { get; set; } = "Bearer";

        /// <summary>
        /// Authentication token or API key
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Username for Basic authentication
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password for Basic authentication
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Header name for API key authentication
        /// </summary>
        public string ApiKeyHeaderName { get; set; } = "X-API-Key";
    }
}
