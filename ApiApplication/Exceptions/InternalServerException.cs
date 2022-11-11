using System;
using System.Net;

namespace ApiApplication.Exceptions
{
    public class InternalServerException:Exception
    {
        public InternalServerException(string error,HttpStatusCode statusCode) : base(error)
        {
            StatusCode = statusCode;

            this.Data[nameof(StatusCode)] = StatusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
    }
}
