using System;
using System.Collections.Generic;

namespace ApiApplication.Models.Showtime
{
    public class Showtime
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public IEnumerable<string> Schedule { get; set; }
        
        public Movie Movie { get; set; }
        
        public int AuditoriumId { get; set; }
    }
}