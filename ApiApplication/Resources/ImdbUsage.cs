using System;
using Newtonsoft.Json;

namespace ApiApplication.Resources
{
    public class ImdbUsage
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("maximum")]
        public int Maximum { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("expireDate")]
        public string? ExpireDate { get; set; }

        [JsonProperty("errorMessage")]
        public string? ErrorMessage { get; set; }
    }
}

