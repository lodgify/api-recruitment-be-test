using ApiApplication.Resources;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiApplication.Commands.ShowtimeCommands.PartiallyUpdateShowtimeCommand
{
    public class PartiallyUpdateShowtimeRequest : IRequest<Showtime>
    {
        public JsonPatchDocument<Showtime> Showtime { get; set; }
    }
}
