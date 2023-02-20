using CinemaApplication.DAL.Models;
using CinemaApplication.DAL.Repositories;
using CinemaApplication.DTOs;
using CinemaApplication.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Concrete
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimeRepository _showtimeRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IImdbService _imdbService;

        public ShowtimeService(IShowtimeRepository showtimeRepository,
            IMovieRepository movieRepository,
            IImdbService imdbService)
        {
            _showtimeRepository = showtimeRepository;
            _movieRepository = movieRepository;
            _imdbService = imdbService;
        }

        public async Task<IEnumerable<ShowtimeDto>> GetAllAsync()
        {
            var showTimes = await _showtimeRepository.GetAllAsync();
            return showTimes.Select(s => new ShowtimeDto
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Schedule = String.Join(",", s.Schedule),
                AudithoriumId = s.AuditoriumId,
                Movie = new MovieDto
                {
                    Title = s.Movie.Title,
                    ImdbId = s.Movie.ImdbId,
                    ReleaseDate = s.Movie.ReleaseDate,
                    Starts = s.Movie.Stars
                }
            });
        }

        public async Task<int> AddAsync(NewShowtimeDto showtime)
        {
            var movie = await _movieRepository.GetAsync(showtime.Movie.ImdbId);
            if (movie == null)
            {
                var imdbMovie = await _imdbService.GetMovieAsync(showtime.Movie.ImdbId);
                movie = new MovieEntity
                {
                    ImdbId = imdbMovie.Id,
                    Title = imdbMovie.Title
                };
            }

            var newShowtimeEntity = await _showtimeRepository.AddAsync(new ShowtimeEntity
            {
                AuditoriumId = showtime.AudithoriumId,
                StartDate = showtime.StartDate,
                EndDate = showtime.EndDate,
                Schedule = showtime.Schedule,
                Movie = movie
            });

            return newShowtimeEntity.Id;
        }

        public async Task DeleteAsync(int showtimeId)
            => await _showtimeRepository.DeleteAsync(showtimeId);

        public async Task UpdateAsync(ShowtimeDto showtime)
        {
            await _showtimeRepository.UpdateAsync(new ShowtimeEntity
            {
                AuditoriumId = showtime.AudithoriumId,
                StartDate = showtime.StartDate,
                EndDate = showtime.EndDate,
                Schedule = showtime.Schedule.Split(','),
                Movie = new MovieEntity
                {
                    ImdbId = showtime.Movie.ImdbId,
                    Title = showtime.Movie.Title,
                    ReleaseDate = showtime.Movie.ReleaseDate
                }
            });
        }
    }
}
