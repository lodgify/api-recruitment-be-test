using BackgroundTaskImdb.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskImdb
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IImdbService _imdbService;

        public Worker(IImdbService imdbService, ILogger<Worker> logger)
        {
            _logger = logger;
            _imdbService = imdbService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var status = await _imdbService.GetImdbStatusAsync();
                _logger.LogInformation("Worker running at: {time} - StatusCode: {status}", DateTimeOffset.Now, status);

                await Task.Delay(3000, stoppingToken);
//                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
