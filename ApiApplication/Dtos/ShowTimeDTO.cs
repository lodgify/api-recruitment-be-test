using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiApplication.Dtos
{
    public class ShowTimeDTO
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }


        [JsonProperty(PropertyName = "movie")]
        public MovieDTO Movie { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "End date is required")]
        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { get; set; }


        [JsonProperty(PropertyName = "schedule")]
        [Required(ErrorMessage = "Schedules are required")]
        public IEnumerable<string> Schedule { get; set; }


        [Required(ErrorMessage = "Auditorium is required")]

        [JsonProperty(PropertyName = "auditorium_id")]
        public int AuditoriumId { get; set; }

    }
}
