using ApiApplication.Models.ViewModels;
using ApiApplication.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new StatusViewModel
            {
                Up = StatusImdb.GetUpImdb(),
                Last_call = StatusImdb.GetLastCall()

            });
        }
    }
}
