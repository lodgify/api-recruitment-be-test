using CinemaApplication.DAL.Abstractions;
using CinemaApplication.DAL.Models;
using CinemaApplication.DAL.Repositories;
using CinemaApplication.DTOs;
using CinemaApplication.Services.Abstractions;
using CinemaApplication.Services.Models;
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
                    Schedule = String.Join(",", s.Schedule),
                    AudithoriumId = s.AuditoriumId,
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
                    if (movieResult.IsError)
                    {
                        return ServiceDataResult<int>.WithError(movieResult.Error);
                    }

                    movie = new MovieEntity
                    {
                        ImdbId = movieResult.Data.Id,
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
                if (showtime.Movie != null &&
                   !string.IsNullOrEmpty(showtime.Movie.ImdbId))
                {
                    var movieResult = await _imdbService.GetMovieAsync(showtime.Movie.ImdbId);
                    if (movieResult.IsError)
                        return ServiceResult.Failure($"Failed to pull movie info.");

                    await _movieRepository.UpdateAsync(new MovieEntity
                    {
                        ImdbId = movieResult.Data.Id,
                        Title = movieResult.Data.Title,
                        ShowtimeId = showtime.Id
                    });
                }

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
