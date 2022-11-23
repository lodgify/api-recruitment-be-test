using Newtonsoft.Json;

namespace ApiApplication.Models.Movies
{
    public class AddMovieModel
    {
        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }
    }
}
