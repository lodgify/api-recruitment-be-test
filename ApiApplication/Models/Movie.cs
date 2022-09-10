using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Models
{
    public class Movie
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("starts")]
        public string Starts { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } //DateTime

        public Movie()
        {
        }
    }
}

