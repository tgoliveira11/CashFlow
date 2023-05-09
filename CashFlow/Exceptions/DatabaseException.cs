using System;
using System.Net;

namespace CashFlow.Api.Exceptions
{
    public class DatabaseException : ApiException
    {
        public DatabaseException(string message) : base(HttpStatusCode.InternalServerError, message) { }

        public DatabaseException(string message, Exception innerException) : base(HttpStatusCode.InternalServerError, message, innerException) { }
    }
}