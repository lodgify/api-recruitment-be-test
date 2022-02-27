using Microsoft.AspNetCore.Mvc;
using ApiApplication.Services;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    public class StatusController : Controller
    {
        private ImdbStatusService _imdbStatusService;

        public StatusController(ImdbStatusService imdbStatusService)
        {
            _imdbStatusService = imdbStatusService;
        }

        [HttpGet]
        [Route("/api/v1/get_imdb_status")]
        public async Task<IActionResult> GetImdbStatus()
        {
            if (ModelState.IsValid)
            {
                return Ok(_imdbStatusService);
            }
            return BadRequest();
        }
    }
}
