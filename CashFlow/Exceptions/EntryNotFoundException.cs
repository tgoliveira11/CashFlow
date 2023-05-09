// EntryNotFoundException.cs
using System;
using System.Net;

namespace CashFlow.Api.Exceptions
{
    public class EntryNotFoundException : ApiException
    {
        public EntryNotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }

        public EntryNotFoundException(string message, Exception innerException) : base(HttpStatusCode.NotFound, message, innerException) { }
    }
}
