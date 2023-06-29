using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Helpers;
using ApiApplication.Models;
using ApiApplication.Services.Implementation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ServiceShowtime : IServiceShowtime
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private readonly IServiceImdbApi _serviceImdbApi;
        public ServiceShowtime(IShowtimesRepository showtimesRepository, IServiceImdbApi serviceImdbApi, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _serviceImdbApi = serviceImdbApi;
            _mapper = mapper;
        }

        public IEnumerable<ShowtimeEntity> GetAll(DateTime? date, string movieTitle)
        {
            if (date is null && movieTitle is null)
                return _showtimesRepository.GetCollection();

            var filter = FilterHelper.ShowtimeFilter(date, movieTitle);
            return _showtimesRepository.GetCollection(filter);
        }

        public ShowtimeEntity GetExisting(int id)
        {
            var filterShowtime = FilterHelper.ShowtimeFilter(id);
            return _showtimesRepository.GetCollection(filterShowtime).FirstOrDefault();
        }

        public async Task<ShowtimeEntity> Add(Showtime showtime)
        {
            var movie = await _serviceImdbApi.GetMovieDetails(showtime.Movie.ImdbId);

            if (movie is null)
                throw new Exception("Movie does not exist, please verify imdb_id value");

            if (movie.Stars is null || movie.Title is null || movie.ReleaseDate is null)
                throw new Exception("Movie does not exist, please verify imdb_id value");

            if (movie is not null)
            {
                showtime.Movie.ReleaseDate = movie.ReleaseDate;
                showtime.Movie.Title = movie.Title;
                showtime.Movie.Stars = movie.Stars;
                var showtimeEntity = _mapper.Map<ShowtimeEntity>(showtime);
                var showtimeResult = _showtimesRepository.Add(showtimeEntity);
                return showtimeResult;
            }
            return null;
        }

        public ShowtimeEntity Remove(int showtimeId)
        {
            return _showtimesRepository.Delete(showtimeId);
        }

        public async Task<ShowtimeEntity> Update(ShowtimeEntity existingShowtime, Showtime showtime)
        {
            var showtimeEntity = _mapper.Map<ShowtimeEntity>(showtime);

            if (showtimeEntity.Movie is not null)
            {
                var updatedMovieId = showtimeEntity.Movie.Id;

                if (showtimeEntity.Movie.ImdbId is not null && !string.Equals(showtimeEntity.Movie.ImdbId, existingShowtime.Movie.ImdbId))
                {
                    var updatedMovie = await _serviceImdbApi.GetMovieDetails(showtime.Movie.ImdbId);
                    if (updatedMovie is not null)
                    {
                        existingShowtime.Movie = _mapper.Map<MovieEntity>(updatedMovie);
                        existingShowtime.Movie.Id = updatedMovieId;
                    }
                }
            }

            existingShowtime.AuditoriumId = showtimeEntity.AuditoriumId;
            existingShowtime.StartDate = showtimeEntity.StartDate;
            existingShowtime.EndDate = showtimeEntity.EndDate;
            existingShowtime.Schedule = showtimeEntity.Schedule;

            return _showtimesRepository.Update(existingShowtime);

        }
    }
}
