using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiApplication.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            _requestDelegate = requestDelegate;
            _logger = loggerFactory.CreateLogger<RequestTimingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {                
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                
                _logger.LogError(ex.Message, "An error occurred. Please try again later.");
                if (context.GetEndpoint() is Endpoint endpoint)
                    if (endpoint.Metadata.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor descriptor){ 
                        _logger.LogError("Error {ErrorStatusCode} at Controller: {ControllerName}, Action: {ActionName} | StackTrace: {StackTrace}",
                        StatusCodes.Status500InternalServerError, descriptor.ControllerName, descriptor.ActionName, ex.StackTrace);
                    }

                var errorResponse = new
                {
                    Error = new{
                        ex.Message
                    }
                };

                var errorJson = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(errorJson);
            }
        }
    }

}
