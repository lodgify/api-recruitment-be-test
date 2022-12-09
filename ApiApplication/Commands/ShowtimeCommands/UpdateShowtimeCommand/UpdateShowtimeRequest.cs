using ApiApplication.Resources;
using MediatR;

namespace ApiApplication.Commands.ShowtimeCommands.UpdateShowtimeCommand
{
    public class UpdateShowtimeRequest : IRequest<Showtime>
    {
        public Showtime Showtime { get; set; }
    }
}

