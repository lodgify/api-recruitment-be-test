using ApiApplication.Application.Command;
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
        private IAddShowTimeCommandHandler _addShowTimeCommandHandler;

        public ShowtimeController(IAddShowTimeCommandHandler addShowTimeCommandHandler)
        {
            _addShowTimeCommandHandler = addShowTimeCommandHandler;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Microsoft.AspNetCore.Mvc.HttpPatch("/api/TestamentoDigital/{guid}/AlterarTipo")]
        public async Task<IActionResult> Patch(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
