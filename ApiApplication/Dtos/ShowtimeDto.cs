using System;
using System.Collections.Generic;

namespace ApiApplication.Dtos
{
    public class ShowtimeDto
    {
        public int Id { get; set; }
        public MovieDto Movie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int AuditoriumId { get; set; }

    }
}
