using System.Collections.Generic;

namespace ApiApplication.Dtos
{
    public class AuditoriumDto
    {
        public int Id { get; set; }
        public List<ShowtimeDto> Showtimes { get; set; }
        public int Seats { get; set; }
    }
}
