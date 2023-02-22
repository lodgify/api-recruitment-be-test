using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Middlewares
{
    /// <summary>
    /// Middleware class that, if injected in the pipeline, it logs all the requests times.
    /// </summary>
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggerMiddleware> _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Call to {context.Request.Path}/{context.Request.Method} at {DateTime.Now}");
            await _next(context);
        }
    }
}
