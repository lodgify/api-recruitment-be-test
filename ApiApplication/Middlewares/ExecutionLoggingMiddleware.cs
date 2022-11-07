using System.Diagnostics;
using System.Threading.Tasks;

using ApiApplication.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Middlewares {
    public class ExecutionLoggingMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<ShowTimeController> _logger;

        public ExecutionLoggingMiddleware(RequestDelegate next, ILogger<ShowTimeController> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Restart();
            await _next(context);
            stopwatch.Stop();
            _logger.LogTrace($"Duration of action {context.Request.Path} was {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
