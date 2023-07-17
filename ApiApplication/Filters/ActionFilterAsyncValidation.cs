using ApiApplication.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Filters
{
    public class ActionFilterAsyncValidation : IAsyncActionFilter
    {

        private readonly ILogger _logger;

        public ActionFilterAsyncValidation(ILogger<ActionFilterAsyncValidation> logger) 
        {
            _logger = logger;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            if (!(ValidatePostShowtime(context) || ValidatePutShowtime(context) || ValidateGetShowTime(context))) 
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
           
            var result = await next();
                 
        }

        private bool ValidateGetShowTime (ActionExecutingContext context)
        {
            return (context.HttpContext.Request.Method == HttpMethod.Get.Method);
        }

        private bool ValidatePostCommandShowtime(ActionExecutingContext context)
        {

            var model = (ShowtimeCommand)context.ActionArguments["command"];

            if (model.Movie == null || model.Id < 1)
            {
                return false;

            }
            return true;
        }

        private bool ValidatePutCommandShowtime(ActionExecutingContext context)
        {

            var model = (ShowtimeCommand)context.ActionArguments["command"];

            if (model.Movie == null)
            {
                return false;

            }
            return true;
        }



        private bool ValidatePostShowtime(ActionExecutingContext context)
        {
            var paramPost = context.ActionArguments.SingleOrDefault(p => p.Value is ShowtimeCommand);
            var methodPost = context.HttpContext.Request.Method == HttpMethod.Post.Method;

            return (paramPost.Value != null
                && methodPost
                && ValidatePostCommandShowtime(context));
          
        }

        private bool ValidatePutShowtime(ActionExecutingContext context)
        {
            var paramPut = context.ActionArguments.SingleOrDefault(p => p.Value is ShowtimeCommand);
            var methodPut = context.HttpContext.Request.Method == HttpMethod.Put.Method;

            return (paramPut.Value != null 
                && methodPut
                && ValidatePutCommandShowtime(context));

        }


    }
}
