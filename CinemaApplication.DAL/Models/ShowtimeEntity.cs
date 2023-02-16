using System;
using System.Collections.Generic;

namespace CinemaApplication.DAL.Models
{
    public class ShowtimeEntity
    {
        public int Id { get; set; }
        public MovieEntity Movie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int AuditoriumId { get; set; }
    }
}
