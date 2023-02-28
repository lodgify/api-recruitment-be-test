using ApiApplication.Core.CQRS;
using Lodgify.Cinema.Domain.Entitie;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.Command
{
    public sealed class AddShowTimeRequest : IRequest
    {
        public int Imdb_id { get; set; }
        public MovieDto Movie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int AuditoriumId { get; set; }
    }

    public  class AddShowTimeDto 
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

    public sealed class MovieDto
    {
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Stars { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int ShowtimeId { get; set; }

        public static implicit operator MovieEntity(MovieDto request)
        {
            return new MovieEntity
            {
                ImdbId = request.ImdbId,
                ReleaseDate = request.ReleaseDate,
                ShowtimeId = request.ShowtimeId,
                Stars = request.Stars,
                Title = request.Title,
            };
        }
    }
}
