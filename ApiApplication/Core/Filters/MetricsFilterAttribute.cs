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

        #endregion [prop]

        public MetricsFilterAttribute()
        {
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// ToDo - test and give more information about the metrics
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _stopwatch.Stop();
            if (!context.HttpContext.Response.Headers.ContainsKey("Lodgify-Response-Time"))
                context.HttpContext.Response.Headers.Add("Lodgify-Response-Time", new Microsoft.Extensions.Primitives.StringValues(_stopwatch.Elapsed.ToString()));
            Console.WriteLine(_stopwatch.Elapsed);
        }

        public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await base.OnResultExecutionAsync(context, next);
            OnResultExecuting(context);
        }
    }
}