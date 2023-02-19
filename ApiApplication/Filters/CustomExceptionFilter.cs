using ApiApplication.DTOs;
using ApiApplication.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;

            if (actionExecutedContext.Exception is CustomException)
            {
                ResponseMessage response = new ResponseMessage(
                        (actionExecutedContext.Exception as CustomException).Key,
                        (actionExecutedContext.Exception as CustomException).Description,
                        ResponseMessageType.Error, ResponseMessagePriority.High,
                        new List<string>(),
                        new List<string>() { (actionExecutedContext.Exception as CustomException).Key, (actionExecutedContext.Exception as CustomException).Description }
                    );

                actionExecutedContext.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            else
            {
                if (actionExecutedContext.Exception.InnerException == null)
                {
                    exceptionMessage = actionExecutedContext.Exception.Message;
                }
                else
                {
                    exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
                }

                var response = new ResponseMessage("InternalServerError", string.Empty, ResponseMessageType.Error, ResponseMessagePriority.High);

                actionExecutedContext.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
