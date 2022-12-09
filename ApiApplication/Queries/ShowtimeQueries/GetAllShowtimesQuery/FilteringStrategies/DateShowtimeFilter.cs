using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Exceptions;
using ApiApplication.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery.FilteringStrategies
{
    public class DateShowtimeFilter : ShowtimeFilter
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private const string messageToMatch = "the current showtimes in projection";
        private readonly IMapper _mapper;

        public DateShowtimeFilter(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public override IEnumerable<Showtime> GetShowtimes(GetAllShowtimesRequest request)
        {
            Func<IQueryable<ShowtimeEntity>, bool> getCollectionfilter = (IQueryable<ShowtimeEntity> query) => {
                return query.All(x => request.Date <= x.EndDate && request.Date >= x.StartDate);
            };
            var showtimes = _showtimesRepository.GetCollection(getCollectionfilter);
            if (showtimes == null || !showtimes.Any())
                throw new NotFoundException(request.Date.GetValueOrDefault(), messageToMatch);

            return _mapper.Map<IEnumerable<Showtime>>(showtimes);
        }
    }
}
