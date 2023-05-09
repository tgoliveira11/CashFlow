﻿using System;
using System.Net;

namespace CashFlow.Api.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
