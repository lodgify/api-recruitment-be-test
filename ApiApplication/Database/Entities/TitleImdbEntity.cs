using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Database.Entities
{
    public class TitleImdbEntity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("stars")]
        public string Stars { get; set; }
        [JsonPropertyName("releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }
}
