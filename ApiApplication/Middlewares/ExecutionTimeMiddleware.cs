using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Middlewares
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExecutionTimeMiddleware(ILogger<ExecutionTimeMiddleware> logger, 
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
         
            context.Response.OnStarting(() => {
                watch.Stop();
                var executionTimeForCompleteRequest = watch.ElapsedMilliseconds;
                
                string controllerName = context.Request.RouteValues["controller"] == null ? "UnspecifiedController" : context.Request.RouteValues["controller"].ToString();
                string actionName = context.Request.RouteValues["action"] == null ? "UnspeciefiedAction" : context.Request.RouteValues["action"].ToString();

                _logger.LogInformation("Execution time for {Controller}/{Action} is {ExecutionTime} ms", controllerName, actionName, executionTimeForCompleteRequest.ToString());
                
                return Task.CompletedTask;
            });

            return this._next(context);
        }
    }
}
