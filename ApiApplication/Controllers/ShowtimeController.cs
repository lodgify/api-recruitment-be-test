using ApiApplication.Application.Command;
using ApiApplication.Application.Querie;
using ApiApplication.Core.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : BaseController
    {
        private readonly IAddShowTimeCommandHandler _addShowTimeCommandHandler;
        private readonly IGetShowTimeQueryHandler _getShowTimeQueryHandler;
        public ShowtimeController(IAddShowTimeCommandHandler addShowTimeCommandHandler,
             IGetShowTimeQueryHandler getShowTimeQueryHandler)
        {
            _addShowTimeCommandHandler = addShowTimeCommandHandler;
            _getShowTimeQueryHandler = getShowTimeQueryHandler;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Microsoft.AspNetCore.Mvc.HttpPost("")]
        public async Task<IActionResult> Post(AddShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => await _addShowTimeCommandHandler.ExecuteAsync(command, cancellationToken));
        }


        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ResponseCache(Duration = 120)]
        [Microsoft.AspNetCore.Mvc.HttpGet("")]
        public async Task<IActionResult> Get(GetShowTimeRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => await _getShowTimeQueryHandler.ExecuteGetAsync(request, cancellationToken),NoContent());
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Microsoft.AspNetCore.Mvc.HttpPatch("")]
        public async Task<IActionResult> Patch(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
