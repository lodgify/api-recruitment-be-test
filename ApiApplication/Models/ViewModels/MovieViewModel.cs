using System;

namespace ApiApplication.Models.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Imdb_id { get; set; }
        public string Starts { get; set; }
        public DateTime Release_date { get; set; }
    }
}
