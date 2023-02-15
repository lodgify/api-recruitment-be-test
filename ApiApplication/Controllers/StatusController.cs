using ApiApplication.Common;
using ApiApplication.Queries.StatusQueries.GetImdbStatusQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IMDBStatus), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImdbStatus()
        {
            var result = await _mediator.Send(new GetImdbStatusRequest());

            return Ok(result);
        }
    }
}
