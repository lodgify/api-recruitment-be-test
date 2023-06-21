using ApiApplication.ImdbApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/{controller}")]
    public class StatusController
    {
        private readonly IStatusInfo _statusInfo;

        public StatusController(IStatusInfo statusInfo)
        {
            _statusInfo = statusInfo;
        }

        [Authorize(Roles = "Read")]
        [HttpGet]
        public HttpStatusCode Get()
        {
            return _statusInfo.GetStatus();
        }
    }
}
