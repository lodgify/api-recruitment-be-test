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

        public AddShowTimeCommandHandler(IShowtimesRepository showtimesRepository,
            CinemaContext dbContext,
            IDomainNotification domainNotification) : base(dbContext, domainNotification)
        {
            _showtimesRepository = showtimesRepository;
            _domainNotification = domainNotification;
        }

        public async Task<AddShowTimeResponse> ExecuteAsync(AddShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                //ToDo - Call IMDB Api to fill movies
                await _showtimesRepository.AddAsync(command, cancellationToken);
                return new AddShowTimeResponse();
            });
        }
    }
}
