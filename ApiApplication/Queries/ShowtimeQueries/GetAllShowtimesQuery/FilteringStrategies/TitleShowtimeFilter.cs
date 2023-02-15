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
    public class TitleShowtimeFilter : ShowtimeFilter
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private const string fieldName = "title";


        public TitleShowtimeFilter(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public override IEnumerable<Showtime> GetShowtimes(GetAllShowtimesRequest request)
        {
            Func<IQueryable<MovieEntity>, bool> moviefilter = (IQueryable<MovieEntity> query) => {
                return query.Where(m => m.Title == request.Title).Any();
            };
            var showtime = _showtimesRepository.GetByMovie(moviefilter);
            if (showtime == null)
                throw new NotFoundException(nameof(Movie), fieldName, request.Title);

            return _mapper.Map<IEnumerable<Showtime>>(new List<ShowtimeEntity> { showtime });

        }
    }
}
