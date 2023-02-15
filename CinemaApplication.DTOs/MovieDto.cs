using System;

namespace CinemaApplication.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Starts { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
