using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.ImdbService.Service;
using ApiApplication.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services.Implementors
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimesRepository showtimesRepository;
        private readonly IMapper mapper;
        private readonly IImdbService imdbService;
        

        public ShowtimeService(IMapper mapper, IShowtimesRepository showtimesRepository, IImdbService imdbService) {
            this.mapper = mapper;
            this.showtimesRepository = showtimesRepository; 
            this.imdbService = imdbService;
        }

        public async Task<Showtime> Create(Showtime showtime)
        {
            showtime.Movie = await ObtainMovieInfo(showtime.Movie.ImdbId);
            var persitedEntity = showtimesRepository.Add(mapper.Map<ShowtimeEntity>(showtime));
            return mapper.Map<Showtime>(persitedEntity);
        }

        public Showtime Delete(int id)
        {
            var deletedEntity = showtimesRepository.Delete(id);
            return mapper.Map<Showtime>(deletedEntity);
        }

        public List<Showtime> GetAll(string movieName, DateTime? date)
        {
            Func<ShowtimeEntity, bool> filter = (entity) =>
            {
                bool isComplaint = true;
                if (!string.IsNullOrEmpty(movieName))
                    isComplaint = movieName.Equals(entity.Movie.Title, StringComparison.InvariantCultureIgnoreCase);
                if (date != null && isComplaint)
                    isComplaint = entity.StartDate <= date && date <= entity.EndDate;
                return isComplaint;
            };
            var entities = showtimesRepository.GetCollection(filter);
            var showtimes = mapper.Map<IEnumerable<ShowtimeEntity>, List<Showtime>>(entities);
            return showtimes;
        }

        public async Task<Showtime> Update(Showtime showtime)
        {
            if (showtime.Movie != null) {
                showtime.Movie = await ObtainMovieInfo(showtime.Movie.ImdbId);
            }
            var persitedEntity = showtimesRepository.Update(mapper.Map<ShowtimeEntity>(showtime));
            return mapper.Map<Showtime>(persitedEntity);

        }

        private async Task<Movie> ObtainMovieInfo(string ImdbId) {
            var movieData = await imdbService.FetchMovieInformation(ImdbId);
            return mapper.Map<Movie>(movieData);
        }
    }
}
