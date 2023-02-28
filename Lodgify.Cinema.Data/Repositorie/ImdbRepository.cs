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
    public sealed partial class ImdbRepository : BaseHttpRepository, IImdbRepository
    {
        private const string ACTION = "movie";

        public ImdbRepository(HttpClient httpClient,
            IPaginatedRequest paginatedRequest,
            ILogger logger) : base(httpClient, logger)
        {
        }

        public async Task<MovieResponse> GetMovieById(int id, CancellationToken cancellationToken)
        {
            string movieId = $"tt{id}";
            var response = await GetAsync<MovieResponse>(ACTION, new MovieRequest { id= movieId } ,cancellationToken);
            return response;
        }

        public struct MovieRequest
        {
            public string id { get; set; }
        }
    }
}