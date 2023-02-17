using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ApiApplication.Middlewares
{
    internal class BenchmarkProcessingTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BenchmarkProcessingTimeMiddleware> _logger;

        public BenchmarkProcessingTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<BenchmarkProcessingTimeMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();   
            await _next(context);
            stopwatch.Stop();

            _logger.LogInformation($"[{context.Request.Method}] {context.Request.Path} processed for {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
