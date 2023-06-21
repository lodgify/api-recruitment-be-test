using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.ImdbApi;
using ApiApplication.ImdbApi.Models;
using ApiApplication.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/{controller}")]
    public class ShowtimeController
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private readonly IImdbApiClient _imdbApiClient;

        public ShowtimeController(IImdbApiClient imdbApiClient, IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
            _imdbApiClient = imdbApiClient;
        }

        [Authorize(Roles = "Read")]
        [HttpGet]
        public async Task<IEnumerable<Showtime>> Get(DateTime? date, string movieTitle)
        {
            IEnumerable<ShowtimeEntity> showtimes = await _showtimesRepository.GetCollectionAsync(showtime =>
                (date == null || showtime.StartDate == date) && 
                (string.IsNullOrEmpty(movieTitle) || showtime.Movie.Title == movieTitle));

            return _mapper.Map<IEnumerable<Showtime>>(showtimes);
        }

        [Authorize(Roles = "Write")]
        [HttpPost]
        public async Task<Showtime> Post([FromBody] Showtime showTime)
        {
            await AssignImdbDataAsync(showTime.Movie);
            ShowtimeEntity showtimeEntity = _mapper.Map<ShowtimeEntity>(showTime);
            showtimeEntity = await _showtimesRepository.AddAsync(showtimeEntity);
            return _mapper.Map<Showtime>(showtimeEntity);
        }

        [Authorize(Roles = "Write")]
        [HttpPut]
        public async Task<Showtime> Put([FromBody] Showtime showTime)
        {
            var showtimeEntity = (await _showtimesRepository.GetCollectionAsync(s => s.Id == showTime.Id)).FirstOrDefault();

            if (showtimeEntity == null)
            {
                throw new Exception("Not found showTimeEntity");
            }

            await AssignImdbDataAsync(showTime.Movie);

            _mapper.Map(showTime, showtimeEntity);

            showtimeEntity = await _showtimesRepository.UpdateAsync(showtimeEntity);

            return _mapper.Map<Showtime>(showtimeEntity);
        }

        [Authorize(Roles = "Write")]
        [HttpDelete]
        public async Task<Showtime> Delete(int id)
        {
            var showtimeEntity = await _showtimesRepository.DeleteAsync(id);

            return _mapper.Map<Showtime>(showtimeEntity);
        }

        [Authorize(Roles = "Write")]
        [HttpPatch]
        public void Patch([FromRoute] int id, [FromBody] JsonPatchDocument showtimeDocument)
        {
            throw new Exception("Simulated 500 error");
        }

        private async Task AssignImdbDataAsync(Movie movie)
        {
            if (movie != null && !string.IsNullOrWhiteSpace(movie.ImdbId))
            {
                ImdbMovie imdbMovie = await _imdbApiClient.GetMovie(movie.ImdbId);
                _mapper.Map(imdbMovie, movie);
            }
        }
    }
}
