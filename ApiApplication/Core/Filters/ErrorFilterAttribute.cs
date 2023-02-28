using Lodgify.Cinema.Domain.Contract.Log;
using Lodgify.Cinema.Domain.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ApiApplication.Core.Filters
{

    public class ErrorFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        #region [prop]

        private readonly ILodgifyLogService _logger;
        private readonly IDomainNotification _domainNotification;

        #endregion [prop]

        #region [ctor]

        public ErrorFilterAttribute(ILodgifyLogService logger, IDomainNotification domainNotification)
        {
            _logger = logger;
            _domainNotification = domainNotification;
        }

        #endregion [ctor]

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null && !filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
                _logger.Log(filterContext.Exception.ToString());
                var result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                filterContext.Result = result;
            }
            else if (_domainNotification.HasNotification)
            {
                var message = string.Join(',', _domainNotification.GetNotifications.Select(s => s));
                var result = new BadRequestObjectResult($"{message}");
                filterContext.Result = result;
            }
        }
    }

}
