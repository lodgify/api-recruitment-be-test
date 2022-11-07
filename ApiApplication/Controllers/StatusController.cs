using System.Threading.Tasks;

using ApiApplication.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers {
    [ApiController]
    [Route("[Controller]")]
    public class StatusController : ControllerBase {
        private readonly ImdbStatus _status;

        public StatusController(ImdbStatus status) {
            _status = status;
        }

        [HttpGet]
        public Task<ImdbStatus> Status() {
            return Task.FromResult(_status);
        }
    }
}
