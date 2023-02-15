using ApiApplication.Database;
using ApiApplication.Exceptions;
using ApiApplication.Resources;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery.FilteringStrategies
{
    public class NoneShowtimeFilter : ShowtimeFilter
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;

        public NoneShowtimeFilter(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public override IEnumerable<Showtime> GetShowtimes(GetAllShowtimesRequest request)
        {
            var showtimes = _showtimesRepository.GetCollection();
            if (showtimes == null || !showtimes.Any())
                throw new NotFoundException(nameof(Showtime));

            return _mapper.Map<IEnumerable<Showtime>>(showtimes);
        }
    }
}
