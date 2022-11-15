using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Filters
{
    public class ActionFilterAsyncLog : IAsyncActionFilter
    {

        private readonly ILogger _logger;

        public ActionFilterAsyncLog(ILogger<ActionFilterAsyncLog> logger) 
        {
            _logger = logger;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            _logger.LogInformation(Constants.Log.TraceFilterControllerStart + context.ActionDescriptor.DisplayName + " "  + DateTime.Now );
           
            var result = await next();
        
            _logger.LogInformation(Constants.Log.TraceFilterControllerEnd + context.ActionDescriptor.DisplayName + " " + DateTime.Now);
        }


    }
}
