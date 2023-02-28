using ApiApplication.Application.Commands;
using ApiApplication.Core.Base;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.Infrastructure.Data.Context;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.Command
{
    public sealed class AddShowTimeCommandHandler : BaseCommandHandler, IAddShowTimeCommandHandler
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IDomainNotification _domainNotification;
        private readonly IImdbRepository _imdbRepository;

        public AddShowTimeCommandHandler(IShowtimesRepository showtimesRepository,
            CinemaContext dbContext,
            IImdbRepository imdbRepository,
            IDomainNotification domainNotification) : base(dbContext, domainNotification)
        {
            _showtimesRepository = showtimesRepository;
            _domainNotification = domainNotification;
            _imdbRepository = imdbRepository;
        }

        /// <summary>
        /// 1375666
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AddShowTimeResponse> ExecuteAsync(AddShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                var movie = await _imdbRepository.GetMovieById(command.Imdb_id,cancellationToken);
                await _showtimesRepository.AddAsync(new Lodgify.Cinema.Domain.Entitie.ShowtimeEntity(), cancellationToken);
                return new AddShowTimeResponse();
            });
        }
    }
}