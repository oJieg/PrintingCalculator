using System.Net;

namespace printing_calculator.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; } 
        public string ErrorMessage { get; }

        public ApiException(HttpStatusCode statusCode, string errorMessage)
            : base(errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public ApiException(HttpStatusCode statusCode, string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
