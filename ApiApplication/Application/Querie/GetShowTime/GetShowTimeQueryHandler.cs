using ApiApplication.Application.Command;
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

            if (request == null || (string.IsNullOrEmpty(request.MovieTitle) && !request.StartDate.HasValue && !request.StartDate.HasValue))
                response = _showtimesRepository.GetCollection();
            else
                response = _showtimesRepository.GetCollection(showTime =>
                       (string.IsNullOrEmpty(request.MovieTitle) || (showTime.Movie != null && showTime.Movie.Title == request.MovieTitle))
                    && (!request.StartDate.HasValue || showTime.StartDate >= request.StartDate)
                    && (!request.EndDate.HasValue) || showTime.EndDate <= request.EndDate);

            if (response == null || !response.Any() || response == default(IEnumerable<GetShowTimeResponse>))
                return null;

            _paginatedRequest.SetLastSince(response.Max(w => w.Id));
            return Convert(response);
        }

        private IEnumerable<GetShowTimeResponse> Convert(IEnumerable<ShowtimeEntity> showTimes)
        {
            foreach (var showTime in showTimes)
                yield return new GetShowTimeResponse()
                {
                    AuditoriumId = showTime.AuditoriumId,
                    EndDate = showTime.EndDate,
                    Schedule = showTime.Schedule,
                    StartDate = showTime.StartDate,
                    Movie = showTime.Movie != null
                            ? new MovieDto
                            {
                                ImdbId = showTime.Movie.ImdbId,
                                ReleaseDate = showTime.Movie.ReleaseDate,
                                ShowtimeId = showTime.Movie.ShowtimeId,
                                Title = showTime.Movie.Title,
                                Stars = showTime.Movie.Stars,
                            }
                            : null
                };
        }


    }
}