using System;
using System.Text.Json.Serialization;

namespace ApiApplication.DTOs.API
{
    public class Movie
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("starts")]
        public string Stars { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
