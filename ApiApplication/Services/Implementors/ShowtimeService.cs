using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Models;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace ApiApplication.Services.Implementors
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimesRepository showtimesRepository;
        private readonly IMapper mapper;
        

        public ShowtimeService(IMapper mapper, IShowtimesRepository showtimesRepository) {
            this.mapper = mapper;
            this.showtimesRepository = showtimesRepository; 
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
    }
}
