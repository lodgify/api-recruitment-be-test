using ApiApplication.Models;
using ApiApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShowTimeController : ControllerBase
    {
        private readonly IShowTimeService _showTimeService;
        public ShowTimeController(IShowTimeService showTimeService) 
        {
            _showTimeService = showTimeService;
        }

        [HttpGet]
        [Authorize(Policy = "ApiKeyReadPolicy")]
        public IActionResult Get([FromQuery(Name = "date")] DateTime? date = null, [FromQuery(Name = "movie_title")] string movieTitle = null)
        {
            var showTimes = _showTimeService.Get(date, movieTitle);

            return Ok(showTimes);
        }

        [HttpPost]
        [Authorize(Policy = "ApiKeyWritePolicy")]
        public async Task<ActionResult<ShowTimeResponseModel>> Post([FromBody] ShowTimeRequestModel showTime)
        {
            var showTimeCreated = await _showTimeService.Create(showTime);

            return CreatedAtAction(nameof(Post), new { id = showTimeCreated.Id }, showTimeCreated);
        }

        [HttpPut]
        [Authorize(Policy = "ApiKeyWritePolicy")]
        public async Task<ActionResult<ShowTimeResponseModel>> Put([FromBody] ShowTimeRequestModel showTime)
        {
            var showTimeUpdated = await _showTimeService.Update(showTime);

            return Ok(showTimeUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "ApiKeyWritePolicy")]
        public ActionResult<ShowTimeResponseModel> Delete([FromRoute] int id)
        {
            _showTimeService.Delete(id);

            return Ok(id);
        }

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Policy = "ApiKeyWritePolicy")]
        public async Task<ActionResult<ShowTimeResponseModel>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ShowTimeRequestModel> showTimePatch)
        {
            var showTimesUpdated = await _showTimeService.Update(id, showTimePatch);

            return Ok(showTimesUpdated);
        }
    }
}
