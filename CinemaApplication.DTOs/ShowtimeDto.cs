using System;
using System.Collections.Generic;

namespace CinemaApplication.DTOs
{
    public class ShowtimeDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Schedule { get; set; }
        public MovieDto Movie { get; set; }
        public int AudithoriumId { get; set; }
    }

    public class NewShowtimeDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<string> Schedule { get; set; } = new List<string>();
        public MovieDto Movie { get; set; }
        public int AudithoriumId { get; set; }
    }
}
