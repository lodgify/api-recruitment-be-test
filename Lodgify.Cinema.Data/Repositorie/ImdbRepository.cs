using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Pagination;
using Lodgify.Cinema.Infrastructure.Data.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Lodgify.Cinema.Infrastructure.Data.Repositorie
{
    public sealed class ImdbRepository : BaseHttpRepository, IImdbRepository
    {
        #region [prop]

        private const string ACTION = "action";

        #endregion [prop]

        #region [ctor]

        public ImdbRepository(HttpClient httpClient,
            IPaginatedRequest paginatedRequest,
            ILogger logger) : base(httpClient, logger)
        {
        }

        #endregion [ctor]
    }
}
