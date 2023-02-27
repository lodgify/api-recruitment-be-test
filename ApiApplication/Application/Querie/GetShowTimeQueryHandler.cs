using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Entitie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.Domain.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.Querie
{
    public sealed class GetShowTimeQueryHandler : IGetShowTimeQueryHandler
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IDomainNotification _domainNotification;
        private readonly IPaginatedRequest _paginatedRequest;
        public GetShowTimeQueryHandler(IShowtimesRepository showtimesRepository,
            IPaginatedRequest paginatedRequest,
            IDomainNotification domainNotification)
        {
            _showtimesRepository = showtimesRepository;
            _domainNotification = domainNotification;
            _paginatedRequest = paginatedRequest;
        }

        public async Task<IEnumerable<GetShowTimeResponse>> ExecuteGetAsync(GetShowTimeRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<ShowtimeEntity> response = null;

            if (request == null && string.IsNullOrEmpty(request.MovieTitle) && !request.StartDate.HasValue && !request.StartDate.HasValue)
                response = _showtimesRepository.GetCollection();

            else if (!string.IsNullOrEmpty(request.MovieTitle) || request.StartDate.HasValue)
                response = _showtimesRepository.GetCollection(showTime => Filter(showTime,request));

            if (response == null || !response.Any() || response == default(IEnumerable<GetShowTimeResponse>))
                return null;

            _paginatedRequest.SetLastSince(response.Max(w => w.Id));
            return Convert(response);
        }

        private bool Filter(ShowtimeEntity showTime, GetShowTimeRequest request)
        {
            return    (showTime.Movie.Title == request.MovieTitle || string.IsNullOrEmpty(request.MovieTitle))
                    && (showTime.StartDate >= request.StartDate || !request.StartDate.HasValue)
                    && (showTime.EndDate <= request.EndDate || !request.EndDate.HasValue);
        }

        private IEnumerable<GetShowTimeResponse> Convert(IEnumerable<ShowtimeEntity> data)
        {
            return null;
        }


    }
}