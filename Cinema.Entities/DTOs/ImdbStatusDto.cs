using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Cinema.Entities.DTOs
{
    public class ImdbStatusDto
    {
        [JsonPropertyName("up")]
        public bool Up { get; set; }
        [JsonPropertyName("last_call")]
        public DateTime LastCall { get; set; }
    }
}
