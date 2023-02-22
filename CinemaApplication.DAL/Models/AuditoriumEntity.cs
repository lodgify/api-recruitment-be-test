using System.Collections.Generic;

namespace CinemaApplication.DAL.Models
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
        public int Seats { get; set; }
    }
}
