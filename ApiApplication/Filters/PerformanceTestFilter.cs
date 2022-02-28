using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using NLog;

namespace ApiApplication.Filters
{
    public class PerformanceTestFilter : IActionFilter
    {
        private Stopwatch stopWatch = new Stopwatch();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopWatch.Stop();
            var executionTime = stopWatch.ElapsedMilliseconds;
            logger.Debug($"executionTime for : {filterContext.RouteData.Values["controller"]} / {filterContext.RouteData.Values["action"]} is : {executionTime} ms.");            
        }
    }
}
