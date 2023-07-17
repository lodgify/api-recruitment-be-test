using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Models.Showtime
{
    public class GetAllShowtimesRequest
    {
        public DateTime? DateTime { get; set; }
        
        public string Title { get; set; }
    }
}