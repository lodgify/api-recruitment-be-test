using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace ApiApplication
{
    public class TimeActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public TimeActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        private Stopwatch stopWatch = new Stopwatch();

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopWatch.Stop();
            var time = stopWatch.ElapsedMilliseconds;
            _logger.LogInformation($"Action: {filterContext.ActionDescriptor.DisplayName}, Time: {time} milliseconds");
        }
    }
}
