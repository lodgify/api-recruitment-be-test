using ApiApplication.DTO;
using ApiApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowtimeController : Controller
    {
     
        private readonly IShowtimeService _service;

        public ShowtimeController(IShowtimeService service)
        {         
            _service= service;
        }

        [Authorize("Write")]
        [Authorize("Read")]
        [HttpGet]
        public IActionResult Get(string title = null, DateTime? date = null)
        {
            var res =  _service.GetShowTimeSchedule(title, date);

            return Ok(res);
        }

        [Authorize("Write")]
        [HttpPost]
        public async Task<IActionResult> Post(ShowtimeCommand command)
        {
            var res = await _service.Add(command);

            return Ok(res);
        }

        [Authorize("Write")]
        [HttpPut]
        public async Task<IActionResult> Put(ShowtimeCommand command)
        {
            var res= await _service.Update(command);

            return Ok(res);

        }

        [HttpPatch]
        public Task<IActionResult> Patch()
        {
            throw new InvalidOperationException(Constants.Exception.PatchMethod);
        }

    }
}
