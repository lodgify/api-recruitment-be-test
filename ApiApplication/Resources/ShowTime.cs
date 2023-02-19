using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Resources
{
    public class ShowTime
    {
#nullable enable
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } //DateTime

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } //DateTime

        [JsonPropertyName("schedule")]
        public string Schedule { get; set; }

#nullable enable
        [JsonPropertyName("movie")]
        public Movie? Movie { get; set; } = null;

        [JsonPropertyName("auditorium_id")]
        public int AuditoriumId { get; set; }

        public ShowTime()
        {
        }
    }
}

