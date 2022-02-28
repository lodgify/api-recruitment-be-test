using ApiApplication.Database;
using ApiApplication.Dtos;
using AutoMapper;
using System.Collections.Generic;

namespace ApiApplication.Services.ShowtimeServices
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        public ShowtimeService (IShowtimesRepository showtimesRepository,
            IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }
        public IEnumerable<ShowtimeDto> GetCollection()
        {
            return _mapper.Map <IEnumerable<ShowtimeDto>>(_showtimesRepository.GetCollection());
        }
    }
}
