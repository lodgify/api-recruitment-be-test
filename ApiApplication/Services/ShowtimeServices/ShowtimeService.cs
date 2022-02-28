using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using ApiApplication.Services.RemoteServices;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services.ShowtimeServices
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private readonly ImdbRemoteService _imdbRemoteService;
        public ShowtimeService (IShowtimesRepository showtimesRepository,
            IMapper mapper,
            ImdbRemoteService imdbRemoteService)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
            _imdbRemoteService = imdbRemoteService;
        }
        public async Task<IEnumerable<ShowtimeDto>> GetCollection()
        {
            var queryResult = await _showtimesRepository.GetCollection();
            return _mapper.Map<IEnumerable<ShowtimeDto>>(queryResult);
        }

        public async Task<ShowtimeDto> Add(ShowtimeDto showtime)
        {
            showtime.Movie = await _imdbRemoteService.GetMovieInformation(showtime.Movie.ImdbId);
            await _showtimesRepository.Add(_mapper.Map<ShowtimeEntity>(showtime));
            return showtime;
        }
    }
}
