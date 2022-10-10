using System;

namespace ApiApplication.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime StartDate { get;}
        public DateTime EndDate { get; set; }   
        public string Schedule { get; set; }
        public int AuditoriumId { get; set; } 
        public Movie Movie { get; set; }  
    }
}
