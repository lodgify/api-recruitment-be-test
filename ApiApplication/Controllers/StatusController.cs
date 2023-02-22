using ApiApplication.Background;
using ApiApplication.DTOs.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ApiApplication.Auth.Constants;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = Roles.Read)]
    public class StatusController : Controller
    {
        [HttpGet("")]
        public ActionResult<IMDBStatus> Get()
        {
            return Ok(IMDBStatusSingleton.Instance.Status);
        }
    }
}
