using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cinema.Entities.DTOs
{
    public class ShowtimeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("schedule")]
        public IEnumerable<string> Schedule { get; set; }
        [JsonPropertyName("movie")]
        public MovieDto Movie { get; set; }
        [JsonPropertyName("auditorium_id")]
        public int AuditoriumId { get; set; }
    }
}
