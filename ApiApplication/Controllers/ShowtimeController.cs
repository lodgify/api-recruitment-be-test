using ApiApplication.Core.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    public class ShowtimeController : BaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
