using ApiApplication.Resources;
using MediatR;

namespace ApiApplication.Commands.ShowtimeCommands.CreateShowtimeCommand
{
    public class CreateShowtimeRequest : IRequest<Showtime>
    {
        public Showtime Showtime { get; set; }
    }
}
