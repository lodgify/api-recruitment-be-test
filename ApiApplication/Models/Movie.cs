using System;

namespace ApiApplication.Models
{
    public class Movie
    {
        public string Title { get; set; }

        public string ImdbId { get; set; }

        public string Starts { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
