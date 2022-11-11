using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Middleware
{
    public class RequestTimeCalculatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimeCalculatorMiddleware> _logger;

        public RequestTimeCalculatorMiddleware(RequestDelegate next, ILogger<RequestTimeCalculatorMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            await _next(httpContext);

            watch.Stop();

            _logger.LogInformation($"Total time to process request in milliseconds is =  {watch.ElapsedMilliseconds}");
        }
    }
}
