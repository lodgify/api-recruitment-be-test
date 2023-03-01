using Lodgify.Cinema.Domain.Entitie;
using System;

namespace ApiApplication.Application.Command
{
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
