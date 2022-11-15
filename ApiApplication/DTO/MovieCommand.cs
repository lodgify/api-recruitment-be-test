using System;

namespace ApiApplication.DTO
{
    public class MovieCommand
    {

        public string Title { get; set; }
        public string Imdb_id { get; set; }
        public string Starts { get; set; }
        public DateTime Release_date { get; set; }

    }
}
