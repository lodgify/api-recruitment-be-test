using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Stars { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
