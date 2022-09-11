using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Middlewares
{
    public sealed class ExecutionTrackerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExecutionTrackerMiddleware> logger;

        public ExecutionTrackerMiddleware(RequestDelegate next, ILogger<ExecutionTrackerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!logger.IsEnabled(LogLevel.Information))
            {
                await this.next(context).ConfigureAwait(false);
                return;
            }

            var watch = new Stopwatch();
            try
            {
                watch.Start();

                await this.next(context).ConfigureAwait(false);
            }
            finally
            {
                watch.Stop();
                var duration = watch.ElapsedMilliseconds;

                logger.LogInformation($"The execution took {duration} ms.");
            }
        }
    }
}

