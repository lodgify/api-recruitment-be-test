using ApiApplication.Core.CQRS;
using Lodgify.Cinema.Domain.Entitie;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.Command
{
    public sealed class AddShowTimeRequest : IRequest
    {
        public int Imdb_id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int Auditorium_Id { get; set; }
    }

    public class AddShowTimeDto
    {
        public MovieDto Movie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int AuditoriumId { get; set; }

        public static implicit operator ShowtimeEntity(AddShowTimeDto request)
        {
            return new ShowtimeEntity
            {
                AuditoriumId = request.AuditoriumId,
                EndDate = request.EndDate,
                Movie = request.Movie,
                Schedule = request.Schedule,
                StartDate = request.StartDate,
            };
        }
    }
}
