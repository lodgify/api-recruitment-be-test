using ApiApplication.Application.Command;
using ApiApplication.Application.Querie;
using ApiApplication.Auth;
using ApiApplication.Core.Base;
using ApiApplication.Core.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : BaseController
    {
        private readonly IAddShowTimeCommandHandler _addShowTimeCommandHandler;
        private readonly IGetShowTimeQueryHandler _getShowTimeQueryHandler;
        private readonly IDeleteShowTimeCommandHandler _deleteShowTimeCommandHandler;
        private readonly IUpdateShowTimeCommandHandler _updateShowTimeCommandHandler;

        public ShowtimeController(IAddShowTimeCommandHandler addShowTimeCommandHandler,
             IGetShowTimeQueryHandler getShowTimeQueryHandler,
             IDeleteShowTimeCommandHandler deleteShowTimeCommandHandler,
             IUpdateShowTimeCommandHandler updateShowTimeCommandHandler)
        {
            _addShowTimeCommandHandler = addShowTimeCommandHandler;
            _getShowTimeQueryHandler = getShowTimeQueryHandler;
            _deleteShowTimeCommandHandler = deleteShowTimeCommandHandler;
            _updateShowTimeCommandHandler = updateShowTimeCommandHandler;
        }

        [Authorize(AuthenticationSchemes = CustomAuthenticationSchemeOptions.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(PaginationFilterAttribute))]
        // [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "MovieTitle", "StartDate", "EndDate", "Since", "PageSize" })]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetShowTimeRequest request, [FromQuery] long since, [FromQuery] string pageSize, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => await _getShowTimeQueryHandler.ExecuteGetAsync(request, cancellationToken), NoContent());
        }

        [Authorize(AuthenticationSchemes = CustomAuthenticationSchemeOptions.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post(AddShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => await _addShowTimeCommandHandler.ExecuteAsync(command, cancellationToken));
        }

        [Authorize(AuthenticationSchemes = CustomAuthenticationSchemeOptions.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> Put(UpdateShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => await _updateShowTimeCommandHandler.ExecuteAsync(command, cancellationToken), NoContent());
        }

        [Authorize(AuthenticationSchemes = CustomAuthenticationSchemeOptions.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => await _deleteShowTimeCommandHandler.ExecuteAsync(command, cancellationToken), NoContent());
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