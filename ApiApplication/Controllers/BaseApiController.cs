using ApiApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {

        }

        protected IActionResult Result<T>(ResponseModel<T> response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}
