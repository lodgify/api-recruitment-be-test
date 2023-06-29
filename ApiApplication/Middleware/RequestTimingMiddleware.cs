using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;
        public RequestTimingMiddleware(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            _requestDelegate = requestDelegate;            
            _logger = loggerFactory.CreateLogger<RequestTimingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _requestDelegate(context);

            stopwatch.Stop();
            var executionTimeSpan = stopwatch.ElapsedMilliseconds;

            if (context.GetEndpoint() is Endpoint endpoint)            
                if (endpoint.Metadata.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor descriptor)
                    _logger.LogInformation("Request was {executionTimeSpan} ms | Controller: {ControllerName}, Action: {ActionName}", 
                    executionTimeSpan, descriptor.ControllerName, descriptor.ActionName);           
            
        }
    }
}
