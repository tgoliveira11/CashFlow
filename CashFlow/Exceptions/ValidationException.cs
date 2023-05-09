using System;
using System.Net;

namespace CashFlow.Api.Exceptions
{
    public class ValidationException : ApiException
    {
        public ValidationException(string message) : base(HttpStatusCode.BadRequest, message) { }

        public ValidationException(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException) { }
    }
}