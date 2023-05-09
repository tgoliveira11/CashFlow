using System;
using System.Net;

namespace CashFlow.Api.Exceptions
{
    public class DuplicateEntryException : ApiException
    {
        public DuplicateEntryException(string message) : base(HttpStatusCode.Conflict, message) { }

        public DuplicateEntryException(string message, Exception innerException) : base(HttpStatusCode.Conflict, message, innerException) { }
    }
}