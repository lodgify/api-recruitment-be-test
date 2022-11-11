using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiApplication.Dtos
{
    public class MovieDTO
    {

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("imdb_id")]
        [Required(ErrorMessage = "IMDB id is required")]
        public string ImdbId { get; set; }

        [JsonPropertyName("stars")]
        public string Stars { get; set; }

        [JsonPropertyName("release_date")]
        [DefaultValue(true)]
        public DateTime? ReleaseDate { get; set; }
    }
}
