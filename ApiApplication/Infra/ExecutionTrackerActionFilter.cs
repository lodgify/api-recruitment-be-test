using System;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Infra {
    
    public class ExecutionTrackerActionFilter : ActionFilterAttribute {

        Stopwatch _stopWatch = new Stopwatch();
        private readonly ILogger<ExecutionTrackerActionFilter> _logger;

        public ExecutionTrackerActionFilter(ILogger<ExecutionTrackerActionFilter> logger) {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            _stopWatch.Reset();
            _stopWatch.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            _stopWatch.Stop();
            _logger.LogTrace($"Duration of action {context.ActionDescriptor.DisplayName} was {_stopWatch.ElapsedMilliseconds} ms");
            base.OnActionExecuted(context);
        }
    }
}
