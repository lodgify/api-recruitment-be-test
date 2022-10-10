using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Models;
using AutoMapper;
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

        public List<Showtime> GetAll()
        {
            var entities = showtimesRepository.GetCollection();
            var showtimes = mapper.Map<IEnumerable<ShowtimeEntity>,List<Showtime>>(entities);
            return showtimes;
        }
    }
}
