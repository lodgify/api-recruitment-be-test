namespace CinemaApplication.Services.Models
{
    public class ImdbApiMovie
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public int Year { get; set; }

        public string SanitizedId
        {
            get
            {
                if (string.IsNullOrEmpty(Id))
                    return Id;

                return this.Id.Replace("/title/", string.Empty)
                    .Replace("/", string.Empty);
            }
        }
    }
}
