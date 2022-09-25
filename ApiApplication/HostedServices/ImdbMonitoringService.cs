using ApiApplication.Helpers;
using ApiApplication.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.HostedServices
{
    internal interface IImdbMonitoringService
    {
        Task CheckAvailability(CancellationToken stoppingToken);
    }

    internal class ImdbMonitoringService : IImdbMonitoringService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly IImdbHelper _imdbHelper;
        private ImdbStatusDto _imdbStatus;

        public ImdbMonitoringService(ILogger<ImdbMonitoringService> logger,
            IImdbHelper imdbHelper,
            ImdbStatusDto imdbStatus)
        {
            _logger = logger;
            _imdbHelper = imdbHelper;
            _imdbStatus = imdbStatus;
        }

        public async Task CheckAvailability(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation("Imdb Monitoring Service is working. Count: {Count}", executionCount);

                var movie = await _imdbHelper.GetMovieInformationByIdAsync("tt1375666");
                if (movie != null)
                {
                    _imdbStatus.Up = true;
                    _imdbStatus.LastCall = DateTime.Now;
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
