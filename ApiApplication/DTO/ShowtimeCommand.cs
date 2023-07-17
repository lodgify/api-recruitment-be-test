
using System;

namespace ApiApplication.DTO
{
    public class ShowtimeCommand
    {

        public int Id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public MovieCommand Movie { get; set; }
        public int Auditorium_id { get; set; }

    }
}
