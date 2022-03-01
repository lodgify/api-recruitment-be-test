using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cinema.Business.ConfigurationHelper
{
    public class AppConfiguration : IAppConfiguration
    {
        public readonly string _baseUrl = string.Empty;
        public readonly string _key = string.Empty;
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _baseUrl = root.GetSection("ImdbApi").GetSection("BaseUrl").Value;
            _key = root.GetSection("ImdbApi").GetSection("Key").Value;

        }
        public string BaseUrl
        {
            get => _baseUrl;
        }

        public string Key
        {
            get => _key;
        }

    }
}
