using System.Text.Json.Serialization;

namespace ApiApplication.DTOs.API
{
    public class Showtime
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("schedule")]
        public string Schedule { get; set; }

        [JsonPropertyName("movie")]
        public Movie Movie { get; set; }

        [JsonPropertyName("auditorium_id")]
        public int AuditoriumId { get; set; }
    }
}
