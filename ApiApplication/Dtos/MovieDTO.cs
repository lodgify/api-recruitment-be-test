using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApiApplication.Dtos
{
    public class MovieDTO
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imdb_id")]
        [Required(ErrorMessage = "IMDB id is required")]
        public string ImdbId { get; set; }

        [JsonProperty("stars")]
        public string Stars { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
