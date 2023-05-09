// ApiExceptionFilterAttribute.cs
using CashFlow.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CashFlow.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiException apiException;
            HttpStatusCode statusCode;
            string message;

            switch (context.Exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = validationException.Message;
                    break;
                case EntryNotFoundException entryNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = entryNotFoundException.Message;
                    break;
                case DuplicateEntryException duplicateEntryException:
                    statusCode = HttpStatusCode.Conflict;
                    message = duplicateEntryException.Message;
                    break;
                case DatabaseException databaseException:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = databaseException.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = context.Exception.Message;
                    break;
            }

            apiException = new ApiException(statusCode, message, context.Exception);
            context.Result = new JsonResult(apiException.Message)
            {
                StatusCode = (int)apiException.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
