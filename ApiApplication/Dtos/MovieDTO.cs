using Newtonsoft.Json;
using System;

namespace ApiApplication.Dtos
{
    public class MovieDTO
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("stars")]
        public string Stars { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
