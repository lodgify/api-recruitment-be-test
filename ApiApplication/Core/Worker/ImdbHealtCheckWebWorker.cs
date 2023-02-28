using Lodgify.Cinema.Domain.Contract;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Core.Worker
{
    public class ImdbHealtCheckWebWorker: BackgroundService
    {
        private const int ONE_MINUTE = 60000;
        private readonly ILogger<BackgroundService> _logger;
        private readonly IImdbStatusService _imdbStatusService;
        private readonly IImdbStatus _imdbStatus;

        public ImdbHealtCheckWebWorker(ILogger<BackgroundService> logger,
            IImdbStatusService imdbStatusService,
            IImdbStatus imdbStatus)
        {
            _logger = logger;
            _imdbStatusService = imdbStatusService;
            _imdbStatus = imdbStatus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var status = await _imdbStatusService.IsUp();
                    _imdbStatus.SetStatus(status.IsUp, DateTime.Now);
                    _logger.LogInformation("ImdbHealtCheckWebWorker running - {time}", DateTimeOffset.Now);
                }
                catch(Exception ex)
                {
                    _imdbStatus.SetException(ex, DateTime.Now);
                }

                await Task.Delay(ONE_MINUTE, stoppingToken);
            }
        }
    }
}