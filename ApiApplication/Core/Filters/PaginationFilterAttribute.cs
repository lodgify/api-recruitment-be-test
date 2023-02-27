﻿using ApiApplication.Core.CQRS;
using ApiApplication.Core.Notification;
using ApiApplication.Core.Pagination;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ApiApplication.Core.Filters
{

    public class PaginationFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        #region [prop]

        private IPaginatedRequest _paginatedRequest;
        private IDomainNotification _domainNotification;

        #endregion [prop]

        #region [ctor]

        public PaginationFilterAttribute(IPaginatedRequest paginatedRequest, IDomainNotification domainNotification)
        {
            _paginatedRequest = paginatedRequest;
            _domainNotification = domainNotification;
        }

        #endregion [ctor]

        /// <summary>
        /// Executed Before controller calls the query
        /// Get the request and set the filter options, filter DI are scoped and living during entire request
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var stringSince = context.HttpContext.Request.Query["since"];
            var stringPageSize = context.HttpContext.Request.Query["pagesize"];

            long since;

            if (!long.TryParse(stringSince, out since))
            {
                _domainNotification.Add("Please, enter a value or a valid value for a querystring since parameter");
            }

            int pagesize;
            if (!int.TryParse(stringPageSize, out pagesize))
            {
                _domainNotification.Add("Please, enter a value or a valid value for a querystring pagesize parameter");
                return;
            }

            _paginatedRequest.SetPagination(since, pagesize);
        }

        /// <summary>
        /// Executed After controller get the result of query
        /// Set the next_page_url, based on pagination last since, only if get success status and
        /// the response is of type IPaginatedResponse
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as OkObjectResult;
            if (result != null)
            {
                var responseLinkResult = result.Value as IPaginatedResponse;
                if (responseLinkResult != null)
                {
                    var requestedUrl = UriHelper.GetDisplayUrl(context.HttpContext.Request);
                    var nextSince = _paginatedRequest.LastSince;
                    responseLinkResult.next_page_url = requestedUrl.ToLower().Replace($"since={_paginatedRequest.Since}", $"since={nextSince}");

                }
            }
        }

        public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            OnResultExecuting(context);
            await base.OnResultExecutionAsync(context, next);
        }

    }
}