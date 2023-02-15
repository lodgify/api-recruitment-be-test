using System;

namespace ApiApplication.Resources
{
    public class Movie
    {
        public int id { get; set; }

        public string title { get; set; }

        public string imdb_id { get; set; }

        public string stars { get; set; }

        public DateTime release_date { get; set; }
    }
}
