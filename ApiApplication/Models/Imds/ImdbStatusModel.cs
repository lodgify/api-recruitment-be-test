
using Newtonsoft.Json;
using System;

namespace ApiApplication.Models.Imds
{
    public class ImdbStatusModel
    {
        public bool Up { get; set; }

        [JsonProperty("last_call")]
        public DateTime LastCall { get; set; }
    }
}
