using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ApiApplication.Filters
{
    public class BanchmarkActionFilter : IActionFilter
    {
        private readonly ILogger<BanchmarkActionFilter> _logger;
        private Stopwatch Timer { get; set; }
        public BanchmarkActionFilter(ILogger<BanchmarkActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Timer = new Stopwatch();

            Timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Timer.Stop();

            var exceutionTime = Timer.ElapsedMilliseconds;

            string actionName = (string)context.RouteData.Values["action"];
            string controller = context.Controller.GetType().Name;

            string message = $"Contoller: {controller}; Action: {actionName}; " +
                             $"Exceution time: {exceutionTime } miliseconds ";

            _logger.LogInformation(message);

        }
    }
}
