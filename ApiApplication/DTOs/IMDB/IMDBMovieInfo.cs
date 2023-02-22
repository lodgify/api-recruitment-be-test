using System;
using System.Text.Json.Serialization;

namespace ApiApplication.DTOs.IMDB
{
    public class IMDBMovieInfo
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("stars")]
        public string Stars { get; set; }
    }
}
