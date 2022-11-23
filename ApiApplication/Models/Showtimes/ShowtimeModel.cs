using ApiApplication.Models.Movies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiApplication.Models.Showtimes
{
    public class ShowtimeModel
    {
        public int Id { get; set; }

        [JsonProperty("start_date ")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date ")]
        public DateTime EndDate { get; set; }
        public List<string> Schedule { get; set; }
        public MovieModel Movie { get; set; }

        [JsonProperty("auditorium_id ")]
        public int AuditoriumId { get; set; }
    }
}
