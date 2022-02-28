using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApiApplication.Services.RemoteServices;
using ApiApplication.Services.ShowtimeServices;
using ApiApplication.Database;
using ApiApplication.Dtos;
using System;
using AutoMapper;
using ApiApplication.Database.Entities;
using ApiApplication.Services;
using System.Collections.Generic;

namespace ApiApplication.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;
        private readonly ImdbRemoteService _imdbRemoteService;
        private IShowtimesRepository _showtimesRepository;
        private IShowtimeService _showtimeService;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IAuditoriumRepository _auditoriumRepository;
        private ImdbStatusService _imdbStatusService;

        public ShowtimeController(ImdbRemoteService imdbRemoteService,
            IShowtimeService showtimeService,
            IShowtimesRepository showtimesRepository,
            IMapper mapper,
            IMovieRepository movieRepository,
            IAuditoriumRepository auditoriumRepository,
            ImdbStatusService imdbStatusService)
        {
            _imdbRemoteService = imdbRemoteService;
            _showtimesRepository = showtimesRepository;
            _showtimeService = showtimeService;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _auditoriumRepository = auditoriumRepository;
            _imdbStatusService = imdbStatusService;
        }
        [HttpGet]
        [Route("/api/v1/get_movie_information")]
        public async Task<IActionResult> GetMovieInformation(
            [FromQuery] string movie_id)
        {
            if (ModelState.IsValid)
            {
                var result = await _imdbRemoteService.GetMovieInformation(movie_id);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/get_all_showtimes")]
        public async Task<object> GetShowTimes(
            [FromQuery] string movie_title
            )
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest();
            }
            
            var showtimes = await _showtimesRepository.GetCollection();
            foreach(var showtime in showtimes)
            {
                showtime.Movie = await _movieRepository.GetShowtimeMovies(showtime.Id);
            }
            return _mapper.Map<IEnumerable<ShowtimeDto>>(showtimes);
        }

        //[HttpGet]
        //[Route("/test")]
        //public async Task<IActionResult> GetShowTimesTest()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = _showtimesRepository.GetCollection();
        //        return Ok(result);
        //    }
        //    return BadRequest();
        //}

        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> CreateShowtime(
            [FromBody] ShowtimeDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_imdbStatusService.Up) { return Problem("The IMDB Service is down."); }
            request.Movie = await _imdbRemoteService.GetMovieInformation(request.Movie.ImdbId);
            await _showtimesRepository.Add(_mapper.Map<ShowtimeEntity>(request));
            await _movieRepository.Add(_mapper.Map<MovieEntity>(request.Movie));
            //await _auditoriumRepository.AddShowtime(request.AuditoriumId, _mapper.Map<ShowtimeEntity>(request));
            return Ok();
        }

        [HttpGet]
        [Route("/get_all_movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            return Ok(await _movieRepository.GetCollection());
        }

        [Route("/get_all_auditoriums")]
        public async Task<IActionResult> GetAllAuditoriums()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var aduditoriums = await _auditoriumRepository.GetCollection();
            foreach(var auditorium in aduditoriums)
            {
                var showtimes = await _showtimesRepository.GetByAuditoriumId(auditorium.Id);
                if (auditorium.Showtimes == null)
                {
                    auditorium.Showtimes = new List<ShowtimeEntity>();
                }
                auditorium.Showtimes = showtimes;
            }
            return Ok(aduditoriums);
        }


    }
}
