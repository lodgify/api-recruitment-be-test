using System;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Quartz.Logging;
using ApiApplication.Exceptions;

namespace ApiApplication.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"There is an error: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // THIS IS A GENERIC WAY TO JUST HANDLE EXCEPTIONS BUT WITH THIS
            // WE CAN'T CONTROL EXPLICIT BUSINESS ERRORS
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //await context.Response.WriteAsync(new ErrorDetails()
            //{
            //    StatusCode = context.Response.StatusCode,
            //    Message = "Opss. Internal Server Error, please contact Support."
            //}.ToString());

            //THIS IS A SECOND WAY TO HANDLE ERRORS, BUT I DON'T LIKE EITHER
            var response = context.Response;

            int statusCode = 0; ;
            string message;

            if (exception is MovieNotFoundException)
            {
                logger.LogInformation(exception, "Moview not found error handled (404).");
                message = exception.Message;
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is MovieFoundException)
            {
                logger.LogInformation(exception, "Movie found error handled (400).");
                message = exception.Message;
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                logger.LogError(exception, "Unhandled exception caught by ExceptionMiddleware. Returning Internal Server Error (500)");
                message = "Opss. Internal Server Error, please contact Support.";
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response
                .WriteAsync(new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString());

            //THERE IS A THIRD WAY TO DO THIS BUT WE CAN DISCUSS IT LATER
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

