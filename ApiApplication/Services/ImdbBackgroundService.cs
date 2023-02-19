using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ImdbBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly ImdbStatusModel _imdbStatusObject;

        public ImdbBackgroundService(ILoggerFactory loggerFactory,
            IHttpClientFactory clientFactory,
            ImdbStatusModel imdbStatusObject)
        {
            _logger = loggerFactory.CreateLogger<ImdbBackgroundService>();
            _httpClient = clientFactory.CreateClient("default");
            _imdbStatusObject = imdbStatusObject;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Calling IMDB API at {DateTime.UtcNow}");

                var response = await _httpClient.GetAsync("https://www.imdb.com/");
                _imdbStatusObject.Status = response.IsSuccessStatusCode;

                await Task.Delay(new System.TimeSpan(0, 0, 5));
            }
        }
    }

    public class ImdbStatusModel
    {
        private bool _status;

        public bool Status
        {
            get { return _status; }

            set { _status = value; }
        }
    }
}
