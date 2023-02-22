using CinemaApplication.Core.Models;
using CinemaApplication.Services.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    /// <summary>
    /// Background service executed every 60 seconds
    /// </summary>
    public class ImdbBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ImdbStatusModel _imdbStatusObject;
        private readonly IImdbService _imdbStatusService;
        private readonly IOptions<ImdbAPIConfig> _imdbAPIConfig;

        public ImdbBackgroundService(
            ILoggerFactory loggerFactory,
            ImdbStatusModel imdbStatusObject,
            IImdbService imdbStatusService,
            IOptions<ImdbAPIConfig> imdbAPIConfig)
        {
            _logger = loggerFactory.CreateLogger<ImdbBackgroundService>();
            _imdbStatusObject = imdbStatusObject;
            _imdbStatusService = imdbStatusService;
            _imdbAPIConfig = imdbAPIConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Calling IMDB API at {DateTime.UtcNow}");

                var result = await _imdbStatusService.GetMovieAsync(_imdbAPIConfig.Value.TestImdbId);
                if (result.IsSuccess)
                {
                    _imdbStatusObject.Up = true;
                    _imdbStatusObject.LastCall = DateTime.UtcNow;
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }

    public class ImdbStatusModel
    {
        public bool Up { get; set; }
        public DateTime LastCall { get; set; }
    }
}
