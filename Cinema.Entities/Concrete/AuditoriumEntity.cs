using Cinema.Core.Entities;
using System.Collections.Generic;

namespace Cinema.Entities.Concrete
{
    public class AuditoriumEntity : IEntity
    {
        public int Id { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
        public int Seats { get; set; }

    }
}
