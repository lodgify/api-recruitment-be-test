using System.Collections.Generic;
using System.Threading.Tasks;

using ApiApplication.DTO;
using ApiApplication.DTO.Queries;
using ApiApplication.Infra;
using ApiApplication.Models;
using ApiApplication.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers {

    [ApiController]
    [Route("[controller]")]
    [ExecutionTracker]
    public class ShowTimeController : ControllerBase {
        private readonly IShowTimeService _showTimeService;

        public ShowTimeController(IShowTimeService showTimeService) {
            _showTimeService = showTimeService;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPolicy")]
        public Task<Result<IEnumerable<ShowTime>>> GetAll([FromQuery] ShowTimeQuery query) =>
            _showTimeService.GetAllAsync(query);

        [HttpPost]
        [Authorize(Policy = "WritePolicy")]
        public Task<Result<ShowTime>> Add(ShowTime item) =>
            _showTimeService.AddAsync(item);

        [HttpPut]
        [Authorize(Policy = "WritePolicy")]
        public Task<Result<ShowTime>> Update(ShowTime item) =>
            _showTimeService.UpdateAsync(item);

        [HttpDelete("{showTimeId}")]
        [Authorize(Policy = "WritePolicy")]
        public Task<Result> Delete(int showTimeId) =>
            _showTimeService.DeleteAsync(showTimeId);

        [HttpPatch]
        [Authorize(Policy = "WritePolicy")]
        public Task Path() {
            throw new System.Exception("An error accoured while pathing the showtime. this error raise just for testing exception handler system");
        }
    }
}
