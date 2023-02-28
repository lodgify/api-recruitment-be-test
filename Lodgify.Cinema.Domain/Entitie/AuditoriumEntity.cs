using System.Collections.Generic;

namespace Lodgify.Cinema.Domain.Entitie
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
        public int Seats { get; set; }

    }
}
