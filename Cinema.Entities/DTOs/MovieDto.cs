using System;
using System.Text.Json.Serialization;

namespace Cinema.Entities.DTOs
{
   
    public class MovieDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }
        [JsonPropertyName("stars")]
        public string Stars { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }
    }
}
