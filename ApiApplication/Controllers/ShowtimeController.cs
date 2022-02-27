using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly ILogger _logger;

        public ShowtimeController(ILogger<ShowtimeController> logger)
        {
            _logger = logger;
        }
        // GET: ShowtimeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShowtimeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShowtimeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShowtimeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowtimeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShowtimeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowtimeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShowtimeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [RouteAttribute("/test")]
        public JsonResult test()
        {
            return Json(0);
        }
    }
}
