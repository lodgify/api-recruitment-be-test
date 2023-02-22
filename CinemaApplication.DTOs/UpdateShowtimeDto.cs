using System;
using System.Collections.Generic;

namespace CinemaApplication.DTOs
{
    public class UpdateShowtimeDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<string> Schedule { get; set; } = new List<string>();
        public string ImdbId { get; set; }
        public int AudithoriumId { get; set; }
    }
}
