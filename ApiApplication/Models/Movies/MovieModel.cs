using Newtonsoft.Json;
using System;

namespace ApiApplication.Models.Movies
{
    public class MovieModel
    {
        public string Title { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        public string Stars { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

    }
}
