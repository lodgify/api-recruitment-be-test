﻿using ApiApplication.Core.CQRS;
using ApiApplication.Core.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Core.Base
{
    [ServiceFilter(typeof(MetricsFilterAttribute))]
    [ServiceFilter(typeof(ErrorFilterAttribute))]
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ExecuteAsync(Func<Task<IResponse>> executeFunction, IActionResult resultForNull = null)
        {
            var response = await executeFunction();

            return response == null
             ? resultForNull ?? BadRequest()
             : Ok(response);
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task<IEnumerable<IResponse>>> executeFunction, IActionResult resultForNull = null)
        {
            var response = await executeFunction();

            return response == null
             ? resultForNull ?? BadRequest()
             : Ok(response);
        }



    }
}
