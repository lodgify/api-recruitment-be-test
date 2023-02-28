using ApiApplication.Application.Commands;
using ApiApplication.Core.Base;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.Infrastructure.Data.Context;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.Command
{
    public sealed class DeleteShowTimeCommandHandler : BaseCommandHandler, IDeleteShowTimeCommandHandler
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IDomainNotification _domainNotification;

        public DeleteShowTimeCommandHandler(IShowtimesRepository showtimesRepository,
            CinemaContext dbContext,
            IDomainNotification domainNotification) : base(dbContext, domainNotification)
        {
            _showtimesRepository = showtimesRepository;
            _domainNotification = domainNotification;
        }

        public async Task<DeleteShowTimeResponse> ExecuteAsync(DeleteShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                if (command == null || command.Id <= 0)
                {
                    _domainNotification.Add("Please, inform an id major than zero");
                    return null;
                }

                _showtimesRepository.Delete(command.Id);
                return new DeleteShowTimeResponse();
            });
        }
    }
}