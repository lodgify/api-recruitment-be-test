using System.Collections.Generic;

namespace ApiApplication.Database.Entities
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
        public int Seats { get; set; }
       
    }
}
