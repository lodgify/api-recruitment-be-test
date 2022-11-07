using System;

namespace ApiApplication.DTO {
    public class ShowTime {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Schedule { get; set; }
        public Movie Movie { get; set; }
        public int AuditoriumId { get; set; }
    }
}
