using ApiApplication.Resources;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Commands.ShowtimeCommands.PartiallyUpdateShowtimeCommand
{
    public class PartiallyUpdateShowtimeHandler : IRequestHandler<PartiallyUpdateShowtimeRequest, Showtime>
    {
        public PartiallyUpdateShowtimeHandler()
        {
        }

        public async Task<Showtime> Handle(PartiallyUpdateShowtimeRequest request, CancellationToken cancellationToken)
        {
            throw new Exception();
        }
    }
}
