using ApiApplication.Models;
using ApiApplication.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IShowTimeService _showTimeService;
        public StatusController(IShowTimeService showTimeService) 
        {
            _showTimeService = showTimeService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "date")] DateTime? date = null, [FromQuery(Name = "movie_title")] string movieTitle = null)
        {
            var showTimes = _showTimeService.Get(date, movieTitle);

            return Ok(showTimes);
        }

        [HttpPost]
        public async Task<ActionResult<ShowTimeResponseModel>> Post([FromBody] ShowTimeRequestModel showTime)
        {
            await _showTimeService.Create(showTime);

            return CreatedAtAction(nameof(Post), new { id = showTime.Id }, showTime);
        }

        [HttpPut]
        public ActionResult<ShowTimeResponseModel> Put([FromBody] ShowTimeRequestModel showTime)
        {
            var showTimes = _showTimeService.Update(showTime);

            return Ok(showTimes);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ShowTimeResponseModel> Delete([FromRoute] int id)
        {
            _showTimeService.Delete(id);

            return Ok(id);
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<ShowTimeResponseModel> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ShowTimeRequestModel> showTimePatch)
        {
            _showTimeService.Delete(id);

            return Ok(id);
        }
    }
}
