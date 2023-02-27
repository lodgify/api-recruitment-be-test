using ApiApplication.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ApiApplication.Core.Filters
{

    public class ErrorFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        #region [prop]

        private readonly ILogger _logger;
        private readonly IDomainNotification _domainNotification;

        #endregion [prop]

        #region [ctor]

        public ErrorFilterAttribute(ILogger logger, IDomainNotification domainNotification)
        {
            _logger = logger;
            _domainNotification = domainNotification;
        }

        #endregion [ctor]

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                _logger.LogError(filterContext.Exception.ToString());
            }

            if (_domainNotification.HasNotification)
            {
                var message = string.Join(',', _domainNotification.GetNotifications.Select(s => s));
                var result = new BadRequestObjectResult($"{message}");
                filterContext.Result = result;
            }
        }
    }

}
