using System;

namespace ApiApplication.Dtos
{
    public class MovieDto
    {
        //public int Id { get; set; }
        //public string Title { get; set; }
        //public string ImdbId { get; set; }
        //public string Stars { get; set; }
        //public DateTime ReleaseDate { get; set; }
        //public int ShowtimeId { get; set; }
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Stars { get; set; }
        public DateTime ReleaseDate { get; set; }

        public MovieDto(string id, DateTime ReleaseDate)
        {
            ImdbId = id;
            ReleaseDate = ReleaseDate;
        }
        public MovieDto()
        {
        }
    }
}
