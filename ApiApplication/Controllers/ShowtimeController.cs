using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApiApplication.Services.RemoteServices;
using ApiApplication.Database;
using ApiApplication.Dtos;
using System;
using AutoMapper;
using ApiApplication.Database.Entities;
using ApiApplication.Services;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApiApplication.Filters;

namespace ApiApplication.Controllers
{
    [ServiceFilter(typeof(PerformanceTestFilter))]
    [ApiController]
    public class ShowtimeController : Controller
    {
        private readonly ImdbRemoteService _imdbRemoteService;
        private ShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IAuditoriumRepository _auditoriumRepository;
        private ImdbStatusService _imdbStatusService;

        public ShowtimeController(ImdbRemoteService imdbRemoteService,
            ShowtimesRepository showtimesRepository,
            IMapper mapper,
            IMovieRepository movieRepository,
            IAuditoriumRepository auditoriumRepository,
            ImdbStatusService imdbStatusService)
        {
            _imdbRemoteService = imdbRemoteService;
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _auditoriumRepository = auditoriumRepository;
            _imdbStatusService = imdbStatusService;
        }
        
        [HttpGet]
        [Route("/showtime")]
        public async Task<object> GetShowTimes(
            [FromQuery] string movie_title,
            [FromQuery] DateTime? date)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest();
            }
            Expression<Func<ShowtimeEntity, bool>> f = (ShowtimeEntity x) => 
                (date != null && x.StartDate <= date && date <= x.EndDate) || 
                (movie_title != null && x.Movie.Title == movie_title) ||
                (date == null && movie_title == null);
            var showtimes =await _showtimesRepository.GetCollection(f);
            foreach(var showtime in showtimes)
            {
                showtime.Movie = await _movieRepository.GetShowtimeMovies(showtime.Id);
            }
            return _mapper.Map<IEnumerable<ShowtimeDto>>(showtimes);
        }

        [HttpPost]
        [Route("/showtime")]
        public async Task<IActionResult> CreateShowtime(
            [FromBody] ShowtimeDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_imdbStatusService.Up) { return Problem("The IMDB Service is down."); }
            request.Movie = await _imdbRemoteService.GetMovieInformation(request.Movie.Imdb_id);
            await _showtimesRepository.Add(_mapper.Map<ShowtimeEntity>(request));
            var movie = _mapper.Map<MovieEntity>(request.Movie);
            await _movieRepository.Add(movie);
            return Ok();
        }

        [HttpPut]
        [Route("/showtime")]
        public async Task<IActionResult> UpdateShowtime(
            [FromBody] ShowtimeDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (request.Movie != null)
            {
                if (!_imdbStatusService.Up) { return Problem("The IMDB Service is down."); }
                request.Movie = await _imdbRemoteService.GetMovieInformation(request.Movie.Imdb_id);
                await _showtimesRepository.Update(_mapper.Map<ShowtimeEntity>(request));
                await _movieRepository.Update(_mapper.Map<MovieEntity>(request.Movie));
            }
            return Ok();
        }

        [HttpDelete]
        [Route("/showtime")]
        public async Task<IActionResult> DeleteShowtime(
            [FromQuery] int showtime_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _showtimesRepository.Delete(showtime_id);
            return Ok();
        }

        [HttpPatch]
        [Route("/showtime")]
        public async Task<IActionResult> PatchShowtime(
            [FromQuery] int showtime_id)
        {
            _showtimesRepository.ThrowErrorMethod();
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

            var result = await _movieRepository.GetCollection();
            return Ok(_mapper.Map<IEnumerable<MovieDto>>(result));
        }

        [Route("/get_all_auditoriums")]
        public async Task<IActionResult> GetAllAuditoriums()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var aduditoriums = await _auditoriumRepository.GetCollection();
            foreach (var auditorium in aduditoriums)
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

        [HttpGet]
        [Route("get_movie_information")]
        public async Task<IActionResult> GetMovieInformation(
            [FromQuery] string movie_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_imdbStatusService.Up) { return Problem("The IMDB Service is down."); }
            var result = await _imdbRemoteService.GetMovieInformation(movie_id);
            return Ok(result);
        }
    }
}
