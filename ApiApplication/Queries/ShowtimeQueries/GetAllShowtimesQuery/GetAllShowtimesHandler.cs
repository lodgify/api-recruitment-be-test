using ApiApplication.Database;
using ApiApplication.Exceptions;
using ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery.FilteringStrategies;
using ApiApplication.Resources;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery
{
    public class GetAllShowtimesHandler : IRequestHandler<GetAllShowtimesRequest, IEnumerable<Showtime>>
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private const string validationMessage = "Showtimes can be filtered by date or movie title but not both";
        private ShowtimeFilter _showtimeFilter { get; set; }

        public GetAllShowtimesHandler(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Showtime>> Handle(GetAllShowtimesRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Title) && request.Date == null)
            {
                _showtimeFilter = new NoneShowtimeFilter(_showtimesRepository, _mapper);
            }
            else if (!string.IsNullOrEmpty(request.Title) && request.Date != null) 
            {
                throw new ValidationException(validationMessage);
            }
            else if (!string.IsNullOrEmpty(request.Title))
            {
                _showtimeFilter = new TitleShowtimeFilter(_showtimesRepository, _mapper);
            }
            else
            {
                _showtimeFilter = new DateShowtimeFilter(_showtimesRepository, _mapper);

            }

            return _showtimeFilter.GetShowtimes(request);

        }

    }
}
