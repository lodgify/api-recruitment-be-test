using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class IMDBHttpClientManager : IIMDBHttpClientManager
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public IMDBHttpClientManager(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _configuration = configuration;

        }

        public async Task<JObject> GetIMDBJObject(string imdbID)
        {
            JObject jObject = null;

            var restClient = _httpClient.CreateClient("IMDBClient");

            string url = $"/Title/{_configuration.GetValue<string>("IMDBApiKey")}/{imdbID}";

            HttpResponseMessage response = await restClient.GetAsync(url);


            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                jObject = JsonConvert.DeserializeObject<JObject>(json);
            }

            return jObject;
        }
    }
}
