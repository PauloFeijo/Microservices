using System;
using System.Net;

namespace Microservice.Producer.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(
            string message,
            string caption = "",
            int statusCode = (int)HttpStatusCode.UnprocessableEntity
            ) : base(message)
        {
            Caption = caption;
            StatusCode = statusCode;
        }

        public string Caption { get; set; }
        public int StatusCode { get; set; }
    }
}
