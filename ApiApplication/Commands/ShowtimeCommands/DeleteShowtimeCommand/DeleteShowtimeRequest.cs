using ApiApplication.Resources;
using MediatR;

namespace ApiApplication.Commands.ShowtimeCommands.DeleteShowtimeCommand
{
    public class DeleteShowtimeRequest : IRequest<Showtime>
    {
        public int Id { get; set; }
    }
}
