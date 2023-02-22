using System.Collections.Generic;

namespace Domain.Entities
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
        public int Seats { get; set; }
    }
}
