using System;
using System.Net;
using System.Threading.Tasks;
using CashFlow.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CashFlow.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errorMessage = "An error occurred while processing the request.";

            if (ex is ApiException apiException)
            {
                statusCode = apiException.StatusCode;
                errorMessage = apiException.Message;
            }

            // Log the exception here if desired

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = errorMessage,
                ExceptionType = ex.GetType().Name,
                StackTrace = ex.StackTrace
            };

            string result = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
    }
}
