using System.Net;

namespace AccomodationWebAPI.CustomExceptions
{
    public class HTTPStatusException : Exception
    {
        public HTTPStatusException() { }

        public HTTPStatusException(string message, HttpStatusCode status)
            : base(message) 
        {
            StatusCode = status;
        }

        public HTTPStatusException(string message, Exception inner, HttpStatusCode status)
            : base(message, inner) 
        {
            StatusCode = status;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
