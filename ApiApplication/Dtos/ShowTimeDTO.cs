using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiApplication.Dtos
{
    public class ShowTimeDTO
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "movie")]
        public MovieDTO Movie { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "schedule")]
        public IEnumerable<string> Schedule { get; set; }

        [JsonProperty(PropertyName = "auditorium_id")]
        public int AuditoriumId { get; set; }

    }
}
