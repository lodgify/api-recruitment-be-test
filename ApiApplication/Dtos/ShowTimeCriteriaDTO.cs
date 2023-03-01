using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Dtos
{
    public class ShowTimeCriteriaDTO
    {
        [JsonPropertyName("show_time")]
        [JsonProperty("show_time")]
        public DateTime? ShowTime { get; set; }

        [JsonPropertyName("movie_title")]
        public string MovieTitle { get; set; }
    }
}
