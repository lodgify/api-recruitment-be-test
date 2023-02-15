using System;

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
}
