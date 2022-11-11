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


        [JsonProperty(PropertyName = "end_date")]
        [Required(ErrorMessage = "End date is required")]       
        public DateTime EndDate { get; set; }


        [JsonProperty(PropertyName = "schedule")]
        [Required(ErrorMessage = "Schedules are required")]
        public IEnumerable<string> Schedule { get; set; }


        [JsonProperty(PropertyName = "auditorium_id")]
        [Required(ErrorMessage = "Auditorium is required")]
        [Range(1,3,ErrorMessage ="Auditorium id should be between 1 to 3")]
        public int AuditoriumId { get; set; }

    }
}
