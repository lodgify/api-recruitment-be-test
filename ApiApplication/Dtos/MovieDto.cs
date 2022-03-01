using System;

namespace ApiApplication.Dtos
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Imdb_id { get; set; }
        public string Stars { get; set; }
        public DateTime Release_date { get; set; }

        public MovieDto()
        {
        }
    }
}
