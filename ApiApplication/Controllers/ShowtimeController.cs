using ApiApplication.Models.Showtimes;
using ApiApplication.Services.Showtimes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiApplication.Controllers
{
    [Route("api/showtime")]
    [ApiController]
    public class ShowtimeController : BaseApiController
    {

        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        [HttpGet]
        [Authorize(Roles = "Read")]
        public IActionResult GetAll(string title, DateTime date)
        {
            var response = _showtimeService.GetAll(title, date);

            return Result(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Read")]
        public IActionResult GetById(int id)
        {
            var response = _showtimeService.GetById(id);

            return Result(response);
        }

        [HttpPost]
        [Authorize(Roles = "Write")]
        public IActionResult Add(AddShowtimeModel model)
        {
            var response = _showtimeService.Add(model);

            return Result(response);
        }


        [HttpDelete]
        [Authorize(Roles = "Write")]
        public IActionResult Delete(int id)
        {
            var response = _showtimeService.Delete(id);

            return Result(response);

        }

        [HttpPut]
        [Authorize(Roles = "Write")]
        public IActionResult Update(ShowtimeModel model)
        {
            var response = _showtimeService.Update(model);

            return Result(response);
        }

        [HttpPatch]
        public IActionResult TestErrorHandling()
        {
            throw new Exception();
        }
    }
}
