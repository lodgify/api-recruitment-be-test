using CinemaApplication.Services.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ImdbBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ImdbStatusModel _imdbStatusObject;
        private readonly IImdbService _imdbStatusService;

        public ImdbBackgroundService(ILoggerFactory loggerFactory,
            ImdbStatusModel imdbStatusObject,
            IImdbService imdbStatusService)
        {
            _logger = loggerFactory.CreateLogger<ImdbBackgroundService>();
            _imdbStatusObject = imdbStatusObject;
            _imdbStatusService = imdbStatusService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Calling IMDB API at {DateTime.UtcNow}");

                var result = await _imdbStatusService.GetImdbStatus();
                if(result.IsSuccessful)
                {
                    _imdbStatusObject.Status = result.Data;
                }

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
