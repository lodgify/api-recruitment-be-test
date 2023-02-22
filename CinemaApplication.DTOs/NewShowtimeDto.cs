using System;
using System.Collections.Generic;

namespace CinemaApplication.DTOs
{
    public class NewShowtimeDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<string> Schedule { get; set; } = new List<string>();
        public NewShowtimeMovieDto Movie { get; set; }
        public int AuditoriumId { get; set; }
    }

    public class NewShowtimeMovieDto
    {
        public string ImdbId { get; set; }
    }
}
