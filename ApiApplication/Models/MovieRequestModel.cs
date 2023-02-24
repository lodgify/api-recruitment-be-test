using ApiApplication.Database.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiApplication.Models
{
    public class MovieRequestModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("stars")]
        public string Stars { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("showtime_id")]
        public int ShowtimeId { get; set; }
    }
}
