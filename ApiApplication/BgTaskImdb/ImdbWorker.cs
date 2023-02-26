using ApiApplication.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.BgTaskImdb
{
    public class ImdbWorker : BackgroundService
    {
        private readonly ILogger<ImdbWorker> _logger;
        private readonly IImdbApiService _imdbService;
        private readonly IImdb _imdb;

        public ImdbWorker(IImdb imdb, IImdbApiService imdbService, ILogger<ImdbWorker> logger)
        {
            _logger = logger;
            _imdbService = imdbService;
            _imdb = imdb;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _imdb.Status = await _imdbService.GetImdbStatusAsync();

                _logger.LogInformation("Worker running at: {time} - StatusCode: {status}", DateTimeOffset.Now, _imdb.Status);

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
