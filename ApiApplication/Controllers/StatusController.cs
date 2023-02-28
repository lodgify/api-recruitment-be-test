using ApiApplication.Auth;
using ApiApplication.Core.Base;
using Lodgify.Cinema.Domain.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseController
    {
        private readonly IImdbStatus _imdbStatus;

        public StatusController(IImdbStatus imdbStatus)
        {
            _imdbStatus = imdbStatus;
        }

        [Authorize(AuthenticationSchemes = CustomAuthenticationSchemeOptions.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_imdbStatus);
        }
    }
}