using ApiApplication.Models.Movies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiApplication.Models.Showtimes
{
    public class AddShowtimeModel
    {
        [JsonProperty("start_date ")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date ")]
        public DateTime EndDate { get; set; }
        public List<string> Schedule { get; set; }
        public AddMovieModel Movie { get; set; }

        [JsonProperty("auditorium_id ")]
        public int AuditoriumId { get; set; }
    }
}
