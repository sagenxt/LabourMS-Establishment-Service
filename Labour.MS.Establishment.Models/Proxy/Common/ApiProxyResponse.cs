using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labour.MS.Establishment.Models.Proxy.Common
{
    public class ApiProxyResponse<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets the error message if any.
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Gets or Sets the data contained in the response.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets a value indicating whether the status code represents a successful response.
        /// </summary>
        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        public TimeSpan ResponseTime { get; set; }

        public static ApiProxyResponse<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK, TimeSpan responseTime = default)
        {
            return new ApiProxyResponse<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = statusCode,
                ResponseTime = responseTime
            };
        }

        public static ApiProxyResponse<T> Failure(string errorMessage, HttpStatusCode statusCode, TimeSpan responseTime = default)
        {
            return new ApiProxyResponse<T>
            {
                IsSuccess = false,
                Error = errorMessage,
                StatusCode = statusCode,
                ResponseTime = responseTime
            };
        }
    }
}
