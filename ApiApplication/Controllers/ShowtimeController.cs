using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    public class ShowtimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
