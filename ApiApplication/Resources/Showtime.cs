using System;
using System.Collections.Generic;

namespace ApiApplication.Resources
{
    public class Showtime
    {
        public int id { get; set; }

        public Movie movie { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public IEnumerable<string> schedule { get; set; }

        public int auditorium_id { get; set; }
    }
}
