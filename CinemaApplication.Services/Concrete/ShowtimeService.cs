using CinemaApplication.Core.Models.ServiceResults;
using CinemaApplication.DAL.Abstractions;
using CinemaApplication.DAL.Models;
using CinemaApplication.DAL.Repositories;
using CinemaApplication.DTOs;
using CinemaApplication.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly IAuditoriumRepository _auditoriumRepository;
        private readonly IImdbService _imdbService;
        private readonly ILogger _logger;

        public ShowtimeService(
            IShowtimeRepository showtimeRepository,
            IMovieRepository movieRepository,
            IAuditoriumRepository auditoriumRepository,
            IImdbService imdbService,
            ILoggerFactory loggerFactory)
        {
            _showtimeRepository = showtimeRepository;
            _movieRepository = movieRepository;
            _auditoriumRepository = auditoriumRepository;
            _imdbService = imdbService;
            _logger = loggerFactory.CreateLogger<ShowtimeService>();
        }

        public async Task<ServiceDataResult<IEnumerable<ShowtimeDto>>> GetAllAsync(ShowtimeQuery query)
        {
            try
            {
                IQueryable<ShowtimeEntity> showtimesQuery = _showtimeRepository.GetQueryable();

                if (!string.IsNullOrEmpty(query.Title))
                {
                    showtimesQuery = showtimesQuery.Where(x => x.Movie.Title.Contains(query.Title, StringComparison.OrdinalIgnoreCase));
                }

                if (query.Date.HasValue)
                {
                    showtimesQuery = showtimesQuery.Where(x => x.StartDate <= query.Date.Value && x.EndDate >= query.Date.Value);
                }

                var showTimes = await showtimesQuery.ToListAsync();

                return ServiceDataResult<IEnumerable<ShowtimeDto>>.WithData(showTimes.Select(s => new ShowtimeDto
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Schedule = s.Schedule,
                    AuditoriumId = s.AuditoriumId,
                    Movie = new MovieDto
                    {
                        Title = s.Movie.Title,
                        ImdbId = s.Movie.ImdbId,
                        ReleaseDate = s.Movie.ReleaseDate,
                        Starts = s.Movie.Stars
                    }
                }));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceDataResult<IEnumerable<ShowtimeDto>>.WithError(ex.Message);
            }
        }

        public async Task<ServiceDataResult<int>> AddAsync(NewShowtimeDto showtime)
        {
            try
            {
                var auditorum = await _auditoriumRepository.GetAsync(showtime.AuditoriumId);
                if (auditorum == null)
                {
                    return ServiceDataResult<int>.WithError("Auditorium is not found.");
                }

                var movie = await _movieRepository.GetAsync(showtime.Movie.ImdbId);
                if (movie == null)
                {
                    var movieResult = await _imdbService.GetMovieAsync(showtime.Movie.ImdbId);
                    if (movieResult.IsError ||
                        movieResult.Data == null)
                    {
                        return ServiceDataResult<int>.WithError(movieResult.Error);
                    }

                    movie = new MovieEntity
                    {
                        ImdbId = movieResult.Data.SanitizedId,
                        Title = movieResult.Data.Title
                    };
                }

                var newShowtimeEntity = await _showtimeRepository.AddAsync(new ShowtimeEntity
                {
                    AuditoriumId = showtime.AuditoriumId,
                    StartDate = showtime.StartDate,
                    EndDate = showtime.EndDate,
                    Schedule = showtime.Schedule,
                    Movie = movie
                });


                return ServiceDataResult<int>.WithData(newShowtimeEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceDataResult<int>.WithError("Failed to add new showtime.");
            }
        }

        public async Task<ServiceResult> DeleteAsync(int showtimeId)
        {
            try
            {
                await _showtimeRepository.DeleteAsync(showtimeId);

                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceResult.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateAsync(ShowtimeDto showtime)
        {
            try
            {
                var showtimeEntity = await _showtimeRepository.GetAsync(showtime.Id);
                if (showtimeEntity == null)
                {
                    return ServiceResult.NotFound($"Showtime #{showtime.Id} not found.");
                }

                var auditorium = await _auditoriumRepository.GetAsync(showtime.AuditoriumId);
                if (auditorium == null)
                {
                    return ServiceResult.NotFound($"Auditorium #{showtime.AuditoriumId} not found.");
                }

                if (showtime.Movie != null &&
                   !string.IsNullOrEmpty(showtime.Movie.ImdbId))
                {
                    var movieResult = await _imdbService.GetMovieAsync(showtime.Movie.ImdbId);
                    if (movieResult.IsError)
                        return ServiceResult.Failure($"Failed to pull movie info.");

                    showtimeEntity.Movie.ImdbId = movieResult.Data.SanitizedId;
                    showtimeEntity.Movie.Title = movieResult.Data.Title;
                }

                showtimeEntity.StartDate = showtime.StartDate;
                showtimeEntity.EndDate = showtime.EndDate;
                showtimeEntity.Schedule = showtime.Schedule;
                showtimeEntity.AuditoriumId = showtime.AuditoriumId;

                await _showtimeRepository.UpdateAsync(showtimeEntity);

                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ServiceResult.Failure(ex.Message);
            }
        }
    }
}
