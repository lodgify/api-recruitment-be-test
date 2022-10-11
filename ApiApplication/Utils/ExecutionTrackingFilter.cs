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

        public void OnActionExecuting(ActionExecutingContext context)
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopWatch.Stop();
            var executionTime = stopWatch.ElapsedMilliseconds;
            Log(context, executionTime);
        }

        private void Log(ActionExecutedContext context, long timeMeasurement)
        {
            var svc = context.HttpContext.RequestServices;
            var logger = svc.GetService<ILogger<ExecutionTrackingFilter>>();
            var controllerName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ActionName;
            logger.LogInformation($"Action [{actionName}] at Controller [{controllerName}]  -> execution lasted {timeMeasurement} ms.");
        }
    }
}
