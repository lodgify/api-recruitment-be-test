using ApiApplication.DTOs;
using ApiApplication.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Filters
{
    public class ValidateModel : ActionFilterAttribute
    {
        private readonly string _validationFailureMessage;

        public ValidateModel(string validationFailureMessage = "")
        {
            _validationFailureMessage = validationFailureMessage;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var response = new ResponseMessage(string.IsNullOrEmpty(_validationFailureMessage) ? "Unsuccessfull Operation" : _validationFailureMessage,
                    string.Empty,
                    ResponseMessageType.Error,
                    ResponseMessagePriority.High,
                    modelState.Keys.Select(l => l.LastIndexOf('.') < 0 ? l : l.Substring(l.LastIndexOf('.') + 1)),
                    modelState.ErrorMessages());

                actionContext.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
