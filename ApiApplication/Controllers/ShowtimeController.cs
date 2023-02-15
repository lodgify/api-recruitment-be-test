using ApiApplication.Commands.ShowtimeCommands.CreateShowtimeCommand;
using ApiApplication.Commands.ShowtimeCommands.DeleteShowtimeCommand;
using ApiApplication.Commands.ShowtimeCommands.PartiallyUpdateShowtimeCommand;
using ApiApplication.Commands.ShowtimeCommands.UpdateShowtimeCommand;
using ApiApplication.Resources;
using ApiApplication.ActionFilters;
using ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("api/showtime")]
    [ServiceFilter(typeof(ExecutionTrackingFilter))]
    public class ShowtimeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShowtimeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Date?}/{Title?}")]
        [Authorize(Policy = "ReadPermission")]
        [ProducesResponseType(typeof(IEnumerable<Showtime>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShowtimes([FromQuery] DateTime? date = null, [FromQuery] string title = null)
        {
            var result = await _mediator.Send(new GetAllShowtimesRequest { Date = date, Title = title });

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Showtime),StatusCodes.Status201Created)]
        [Authorize(Policy = "WritePermission")]
        public async Task<IActionResult> CreateShowtime([FromBody] CreateShowtimeRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Showtime), StatusCodes.Status200OK)]
        [Authorize(Policy = "WritePermission")]
        public async Task<IActionResult> UpdateShowtime([FromBody] UpdateShowtimeRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Showtime), StatusCodes.Status200OK)]
        [Authorize(Policy = "WritePermission")]
        public async Task<IActionResult> DeleteShowtime([FromBody] DeleteShowtimeRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(Showtime), StatusCodes.Status200OK)]
        [Authorize(Policy = "WritePermission")]
        public async Task<IActionResult> PartiallyUpdateShowtime([FromBody] PartiallyUpdateShowtimeRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
