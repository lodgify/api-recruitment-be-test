namespace CinemaApplication.Services.Models
{
    public class ImdbMovie
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public int Year { get; set; }
    }
}
