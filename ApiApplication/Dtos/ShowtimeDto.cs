using System;
using System.Collections.Generic;

namespace ApiApplication.Dtos
{
    public class ShowtimeDto
    {
        public int Id { get; set; }
        public MovieDto Movie { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int Auditorium_id { get; set; }

        public ShowtimeDto()
        {
        }

    }
}
