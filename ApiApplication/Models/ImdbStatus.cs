using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Models
{
    public class ImdbStatus
    {
        [JsonPropertyName("up")]
        public bool Up { get; set; }

        [JsonPropertyName("last_call")]
        public string LastCall { get; set; } //DateTime

        public ImdbStatus()
        {
        }
    }
}

