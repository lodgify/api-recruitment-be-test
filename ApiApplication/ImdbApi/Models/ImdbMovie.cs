using System;

namespace ApiApplication.ImdbApi.Models
{
    public class ImdbMovie
    {
        public string Title { get; set; }

        public string Stars { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
