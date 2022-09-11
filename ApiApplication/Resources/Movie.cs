using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Resources
{
    public class Movie
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; } = null;

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("stars")]
        public string? Stars { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; } //DateTime

        public Movie()
        {
        }
    }
}

