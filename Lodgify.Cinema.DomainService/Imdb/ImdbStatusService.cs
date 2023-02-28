using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using System;
using System.Threading.Tasks;

namespace Lodgify.Cinema.DomainService.Imdb
{
    public class ImdbStatusService : IImdbStatusService
    {
        private readonly IImdbRepository _imdbRepository;

        public ImdbStatusService(IImdbRepository imdbRepository)
        {
            _imdbRepository = imdbRepository;
        }

        public async Task<IImdbStatus> IsUp()
        {
            var isOk = await _imdbRepository.HealtCheckStatus();
            return new ImdbStatus(isOk);
        }
    }
}