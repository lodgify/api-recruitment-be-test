using Cinema.Business.Abstract;
using Cinema.Business.Constants;
using Cinema.Core.Utilities.Results;
using Cinema.DataAccess.Abstract;
using Cinema.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Cinema.Business.ConfigurationHelper;
using Cinema.Entities.DTOs;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Business.Concrete
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IAuditoriumEntityDal _auditoriumEntityDal;
        private readonly IShowtimeEntityDal _showtimeEntityDal;
        private readonly IMovieEntityDal _movieEntityDal;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppConfiguration _appConfiguration;
        private readonly IMapper _mapper;

        public ShowtimeService(IHttpClientFactory httpClientFactory, IAppConfiguration appConfiguration, IMapper mapper, IShowtimeEntityDal showtimeEntityDal, IMovieEntityDal movieEntityDal, IAuditoriumEntityDal auditoriumEntityDal)
        {
            _httpClientFactory = httpClientFactory;
            _appConfiguration = appConfiguration;
            _showtimeEntityDal = showtimeEntityDal;
            _movieEntityDal = movieEntityDal;
            _auditoriumEntityDal = auditoriumEntityDal;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds showtime to auditorium.
        /// </summary>
        /// <param name="showtimeDto">Showtime information to add.</param>
        /// <returns>Showtime information with auditorium id</returns>
        public async Task<IResult> AddAsync(ShowtimeDto showtimeDto)
        {
            // Checks if auditorium exists.
            var auditorium = await _auditoriumEntityDal.Include(x => x.Showtimes).FirstOrDefaultAsync(x => x.Id == showtimeDto.AuditoriumId);
            if (auditorium == null)
            {
                return new ErrorResult(Messages.AuditoriumNotFound);
            }

            // Checks if requested showtime's dates overlaps with the other showtimes inside the auditorium.
            bool dateOverlap = auditorium.Showtimes.Any(x => x.StartDate <= showtimeDto.EndDate && showtimeDto.StartDate <= x.EndDate);
            if (dateOverlap)
            {
                return new ErrorResult(Messages.ShowtimeOverlap);
            }

            var showtime = _mapper.Map<ShowtimeEntity>(showtimeDto);

            // If we have already have the movie with the same imdb id inside our db then use it. Otherwise get movie information from imdb api.
            var existingMovie = await _movieEntityDal.GetAsync(x => x.ImdbId == showtime.Movie.ImdbId);
            if (existingMovie != null)
            {
                MovieEntity movie = new MovieEntity()
                {
                    Id = 0,
                    ImdbId = existingMovie.ImdbId,
                    ReleaseDate = existingMovie.ReleaseDate,
                    Stars = existingMovie.Stars,
                    Title = existingMovie.Title
                };
                showtime.Movie = movie;
            }
            else
            {
                var result = await _httpClientFactory.CreateClient("CinemaHttpClient").GetAsync($"{_appConfiguration.BaseUrl}/{_appConfiguration.Key}/{showtime.Movie.ImdbId}");
                if (result.IsSuccessStatusCode)
                {
                    var stringResponse = await result.Content.ReadAsStringAsync();
                    var movieDtoFromImdb = JsonConvert.DeserializeObject<ImdbMovieDto>(stringResponse);
                    if (!string.IsNullOrWhiteSpace(movieDtoFromImdb.ErrorMessage))
                    {
                        return new ErrorResult(movieDtoFromImdb.ErrorMessage);
                    }
                    var movieEntity = _mapper.Map<MovieEntity>(movieDtoFromImdb);
                    var movieEntityAdded = await _movieEntityDal.AddAsync(movieEntity);
                    showtime.Movie = movieEntityAdded;
                }
                else
                {
                    return new ErrorResult(Messages.ImdbApiError);
                }

            }

            var showTimeFinal = await _showtimeEntityDal.AddAsync(showtime);

            auditorium.Showtimes.Add(showTimeFinal);
            await _auditoriumEntityDal.UpdateAsync(auditorium);

            var showtimeDtoResult = _mapper.Map<ShowtimeDto>(showTimeFinal);
            showtimeDtoResult.AuditoriumId = auditorium.Id;

            return new SuccessDataResult<ShowtimeDto>(showtimeDtoResult, Messages.ShowtimeAdded);
        }

        /// <summary>
        /// Deletes the showtime.
        /// </summary>
        /// <param name="id">Showtime Id</param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(int id)
        {
            var showtimeEntity = await _showtimeEntityDal.Include(x => x.Movie).FirstOrDefaultAsync(c => c.Id == id);
            if (showtimeEntity != null)
            {
                var auditorium = await FindAuditoriumWhereShowtimeExists(showtimeEntity);
                // Removing requested showtime from the auditorium then deleting the showtime entity.
                auditorium.Showtimes.Remove(showtimeEntity);
                await _auditoriumEntityDal.UpdateAsync(auditorium);
                await _showtimeEntityDal.DeleteAsync(showtimeEntity);

                var showtimeDtoResult = _mapper.Map<ShowtimeDto>(showtimeEntity);
                showtimeDtoResult.AuditoriumId = auditorium.Id;

                return new SuccessDataResult<ShowtimeDto>(showtimeDtoResult, Messages.ShowtimeDeleted);
            }
            return new ErrorResult(Messages.EntityNotFound);

        }

        /// <summary>
        /// Updates showtime entity(also auditorium entity when necessary).
        /// </summary>
        /// <param name="id">Showtime Id</param>
        /// <param name="showtimeDto">Showtime information to update the entity</param>
        /// <returns>Showtime information with auditorium id</returns>
        public async Task<IResult> UpdateAsync(int id, ShowtimeDto showtimeDto)
        {
            // Checking if the requested auditorium exists
            var auditoriumToUpdate = await _auditoriumEntityDal.Include(x => x.Showtimes).FirstOrDefaultAsync(x => x.Id == showtimeDto.AuditoriumId);
            if (auditoriumToUpdate == null)
            {
                return new ErrorResult(Messages.AuditoriumNotFound);
            }

            // Getting the entity to update
            var showtimeEntity = await _showtimeEntityDal.Include(x => x.Movie).FirstOrDefaultAsync(c => c.Id == id);
            if (showtimeEntity != null)
            {
                // Checking if any date ranges overlaps for the requested auditorium.
                bool dateOverlap = auditoriumToUpdate.Showtimes.Where(x => x.Id != id).Any(x => x.StartDate <= showtimeDto.EndDate && showtimeDto.StartDate <= x.EndDate);
                if (dateOverlap)
                {
                    return new ErrorResult(Messages.ShowtimeOverlap);
                }

                // If dto has movie data then use imdb id to get the requested data from imdb api and update movie information of the showtime.
                if (showtimeDto.Movie != null)
                {
                    var result = await _httpClientFactory.CreateClient("CinemaHttpClient").GetAsync($"{_appConfiguration.BaseUrl}/{_appConfiguration.Key}/{showtimeDto.Movie.ImdbId}");
                    if (result.IsSuccessStatusCode)
                    {
                        var stringResponse = await result.Content.ReadAsStringAsync();
                        var movieDtoFromImdb = JsonConvert.DeserializeObject<ImdbMovieDto>(stringResponse);
                        if (!string.IsNullOrWhiteSpace(movieDtoFromImdb.ErrorMessage))
                        {
                            return new ErrorResult(movieDtoFromImdb.ErrorMessage);
                        }
                        var movieEntity = _mapper.Map<MovieEntity>(movieDtoFromImdb);
                        showtimeEntity.Movie = movieEntity;
                    }
                    else
                    {
                        return new ErrorResult(Messages.ImdbApiError);
                    }
                }
            
                // Checking the current auditorium that showtime exists.
                var auditoriumWhereShowtimeExists = await FindAuditoriumWhereShowtimeExists(showtimeEntity);

                // If current auditorium is different from requsted auditorium then move the showtime entity to requested auditorum.
                if (auditoriumWhereShowtimeExists.Id != showtimeDto.AuditoriumId)
                {
                    auditoriumWhereShowtimeExists.Showtimes.Remove(showtimeEntity);
                    auditoriumToUpdate.Showtimes.Add(showtimeEntity);
                    await _auditoriumEntityDal.UpdateAsync(auditoriumWhereShowtimeExists);
                    await _auditoriumEntityDal.UpdateAsync(auditoriumToUpdate);
                }
       
                showtimeEntity.Schedule = showtimeDto.Schedule;
                showtimeEntity.StartDate = showtimeDto.StartDate;
                showtimeEntity.EndDate = showtimeDto.EndDate;

                await _showtimeEntityDal.UpdateAsync(showtimeEntity);

                var showtimeDtoResult = _mapper.Map<ShowtimeDto>(showtimeEntity);
                showtimeDtoResult.AuditoriumId = auditoriumToUpdate.Id;

                return new SuccessDataResult<ShowtimeDto>(showtimeDtoResult, Messages.ShowtimeUpdated);
            }
            return new ErrorResult(Messages.EntityNotFound);
        }

        /// <summary>
        /// Getting showtimes with related auditorium ids.
        /// </summary>
        /// <param name="movieTitle">List filtered by title(optional).</param>
        /// <param name="date">List filtered by date(optional).</param>
        /// <returns>List of showtimes including auditorium ids.</returns>
        public async Task<IDataResult<List<ShowtimeDto>>> GetShowtimes(string movieTitle = null, DateTime? date = null)
        {
            IQueryable<ShowtimeEntity> query = _showtimeEntityDal.GetQueryable();

            if (movieTitle != null)
            {
                query = _showtimeEntityDal.GetQueryable(x => x.Movie.Title == movieTitle);
            }

            if (date != null)
            {
                query = _showtimeEntityDal.GetQueryable(x => x.StartDate <= date && x.EndDate >= date);
            }

            var result = await query.Include(x => x.Movie).ToListAsync();

            List<ShowtimeDto> showtimeDtos = new List<ShowtimeDto>();

            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    var res = await FindAuditoriumWhereShowtimeExists(item);
                    showtimeDtos.Add(new ShowtimeDto() { AuditoriumId = res.Id, Movie = _mapper.Map<MovieDto>(item.Movie), Id = item.Id, StartDate = item.StartDate, EndDate = item.EndDate, Schedule = item.Schedule });
                }
                return new SuccessDataResult<List<ShowtimeDto>>(showtimeDtos, Messages.ShotimeListSuccess);
            }

            return new ErrorDataResult<List<ShowtimeDto>>(Messages.ShowtimeNotFound);


        }

        /// <summary>
        /// Gets auditorium information of the specified showtime.
        /// </summary>
        /// <param name="showtimeEntity">Showtime to retrieve related auditorium info. </param>
        /// <returns>Auditorium</returns>
        private async Task<AuditoriumEntity> FindAuditoriumWhereShowtimeExists(ShowtimeEntity showtimeEntity)
        {           
            var res = await _auditoriumEntityDal.Include(y => y.Showtimes).FirstOrDefaultAsync(x => x.Showtimes.Contains(showtimeEntity));
            return res;
        }



    }
}
