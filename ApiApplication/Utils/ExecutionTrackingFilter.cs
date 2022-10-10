using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ApiApplication.Utils
{
    public class ExecutionTrackingFilter : IActionFilter
    {
        private Stopwatch stopWatch = new Stopwatch();
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var svc = context.HttpContext.RequestServices;
            var logger = svc.GetService<ILogger<ExecutionTrackingFilter>>();
            stopWatch.Stop();
            var controllerName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ActionName;
            var executionTime = stopWatch.ElapsedMilliseconds;
            logger.LogInformation($"Action [{actionName}] at Controller [{controllerName}]  -> execution lasted {executionTime} ms.");
        }
    }
}
