using ApiApplication.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : Controller
    {

        [HttpGet]
        public IActionResult Get()
        {         
            return Ok( new StatusDTO
            {
                Up = StatusImdb.GetUpImdb(),
                Last_call = StatusImdb.GetLastCall()
            
            });
        }
    }
}
