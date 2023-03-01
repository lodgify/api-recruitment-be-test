using Lodgify.Cinema.Domain.Contract.Log;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ApiApplication.Core.Filters
{
    public class MetricsFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        #region [prop]

        private readonly Stopwatch _stopwatch;
        private readonly ILodgifyLogService _lodgifyLogService;

        #endregion [prop]

        public MetricsFilterAttribute(ILodgifyLogService lodgifyLogService)
        {
            _stopwatch = new Stopwatch();
            _lodgifyLogService = lodgifyLogService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _stopwatch.Stop();
            string log = GetRequestInformation(context);
            if (!context.HttpContext.Response.Headers.ContainsKey("Lodgify-Response-Time"))
                context.HttpContext.Response.Headers.Add("Lodgify-Response-Time", new Microsoft.Extensions.Primitives.StringValues(_stopwatch.Elapsed.ToString()));
            _lodgifyLogService.Log(log);
        }

        public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await base.OnResultExecutionAsync(context, next);
            OnResultExecuting(context);
        }

        private string GetRequestInformation(ResultExecutingContext context)
        {
            string controller = GetRouteDataValue("Controller", context);
            string action = GetRouteDataValue("Action", context);
            string route = $"{controller}/{action}";
            var timeElapsed = _stopwatch.Elapsed;
            string method = context.HttpContext.Request.Method;

            return $@"
Route:{route}
Type:{method}
Elapsed:{timeElapsed}";
        }

        private string GetRouteDataValue(string routeDataValue, FilterContext context)
        {
            try
            {
                var val = context.RouteData.Values[routeDataValue];
                return val == null ? string.Empty : val.ToString();
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}