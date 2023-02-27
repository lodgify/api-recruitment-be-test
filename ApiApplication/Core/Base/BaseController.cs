using ApiApplication.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Core.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ExecuteAsync(Func<Task<IResponse>> executeFunction, IActionResult resultForNull = null)
        {
            var response = await executeFunction();

            return response == null
             ? resultForNull ?? BadRequest()
             : Ok(response);
        }
    }
}
