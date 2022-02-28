using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Extensions
{
    public class ExecutionTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger logger;

        public ExecutionTrackingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
            logger = _loggerFactory.CreateLogger("ExecutionTracking");
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();
            logger.LogInformation($"Time elapsed (For): {sw.Elapsed.ToString("mm\\:ss\\.ff")}");

        }
    }
}
