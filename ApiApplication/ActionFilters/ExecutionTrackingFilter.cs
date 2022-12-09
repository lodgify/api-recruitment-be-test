using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ApiApplication.ActionFilters
{

    public class ExecutionTrackingFilter : ActionFilterAttribute
    {
        private Stopwatch stopWatch = new Stopwatch();
        private readonly ILogger<ExecutionTrackingFilter> _logger;

        public ExecutionTrackingFilter(ILogger<ExecutionTrackingFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatch.Reset();
            stopWatch.Start();
            _logger.LogInformation("Tracking the execution time..");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopWatch.Stop();
            _logger.LogInformation($"Execution time of {filterContext.HttpContext.Request.Method} method for the uri {filterContext.HttpContext.Request.Path} : {stopWatch.ElapsedMilliseconds} miliseconds");
        }
    }
}
