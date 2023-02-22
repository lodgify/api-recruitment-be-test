using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Base cinema API controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CinemaBaseApiController : ControllerBase
    { }
}
