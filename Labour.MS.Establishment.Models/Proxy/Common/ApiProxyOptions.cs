
namespace Labour.MS.Establishment.Models.Proxy.Common
{
    /// <summary>
    /// Configuration options for the API proxy
    /// </summary>
    public class ApiProxyOptions
    {
        public const string SectionName = "ApiProxy";

        /// <summary>
        /// Base URL for the target microservice
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Default timeout for HTTP requests
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Maximum number of retry attempts
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 3;

        /// <summary>
        /// Delay between retry attempts (in milliseconds)
        /// </summary>
        public int RetryDelayMs { get; set; } = 1000;

        /// <summary>
        /// Whether to use exponential backoff for retries
        /// </summary>
        public bool UseExponentialBackoff { get; set; } = true;

        /// <summary>
        /// Default headers to include with every request
        /// </summary>
        public Dictionary<string, string> DefaultHeaders { get; set; } = new();

        /// <summary>
        /// Authentication configuration
        /// </summary>
        public AuthenticationOptions Authentication { get; set; } = new();

        /// <summary>
        /// Whether to log request/response details
        /// </summary>
        public bool EnableLogging { get; set; } = true;

        /// <summary>
        /// Whether to log sensitive data (headers, body content)
        /// </summary>
        public bool LogSensitiveData { get; set; } = false;
    }
}
