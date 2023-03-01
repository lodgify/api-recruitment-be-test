using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiApplication.Dtos
{
    public class ShowTimeDTO
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("movie")]
        public MovieDTO? Movie { get; set; }

        [JsonPropertyName("start_date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }


        [JsonPropertyName("end_date")]
        [Required(ErrorMessage = "End date is required")]       
        public DateTime EndDate { get; set; }


        [JsonPropertyName("schedule")]
        [Required(ErrorMessage = "Schedules are required")]
        public IEnumerable<string> Schedule { get; set; }


        [JsonPropertyName("auditorium_id")]
        [Required(ErrorMessage = "Auditorium is required")]
        [Range(1,3,ErrorMessage ="Auditorium id should be between 1 to 3")]
        public int AuditoriumId { get; set; }

    }
}
