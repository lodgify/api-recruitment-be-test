using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Pagination;
using Lodgify.Cinema.Infrastructure.Data.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static Lodgify.Cinema.Domain.Dto.ImdbRepository;

namespace Lodgify.Cinema.Infrastructure.Data.Repositorie
{
    public sealed class ImdbRepository : BaseHttpRepository, IImdbRepository
    {
        private const string ACTION = "movie";
        private readonly IImdbIdTranslatorService _imdbIdTranslatorService;

        public ImdbRepository(HttpClient httpClient,
            IImdbIdTranslatorService imdbIdTranslatorService,
            IPaginatedRequest paginatedRequest,
            ILogger logger) : base(httpClient, logger)
        {
            _imdbIdTranslatorService = imdbIdTranslatorService;
        }

        public async Task<MovieResponse> GetMovieByIdAsync(int id, CancellationToken cancellationToken) => await GetAsync<MovieResponse>(ACTION, new MovieRequest { id = _imdbIdTranslatorService.Get(id) }, cancellationToken);
        public async Task<bool> HealtCheckStatusAsync(CancellationToken cancellationToken) => await IsSuccessStatusCodeAsync(ACTION, new MovieRequest { id = _imdbIdTranslatorService.Get(1375666) }, cancellationToken);
    }
}