using ApiApplication.Core.Base;
using ApiApplication.Core.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [ServiceFilter(typeof(MetricsFilterAttribute))]
    public class ShowtimeController : BaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
