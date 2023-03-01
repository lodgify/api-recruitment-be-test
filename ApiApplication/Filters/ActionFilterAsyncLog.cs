using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using ApiApplication.Database;
using System.Diagnostics;

namespace ApiApplication.Filters
{
    public class ActionFilterAsyncLog : IAsyncActionFilter
    {
        private readonly ILogger _logger;

        public ActionFilterAsyncLog(ILogger<ActionFilterAsyncLog> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _ = await next();
            _logger.LogInformation(Constants.Log.TraceFilterControllerExecutionTime + context.ActionDescriptor.DisplayName + " " + stopWatch.Elapsed);
            stopWatch.Reset();
        }
    }
}
