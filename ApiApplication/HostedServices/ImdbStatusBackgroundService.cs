using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.HostedServices
{
    public class ImdbStatusBackgroundService : BackgroundService
    {
        private readonly ILogger<ImdbStatusBackgroundService> _logger;
        public IServiceProvider Services { get; }

        public ImdbStatusBackgroundService(IServiceProvider services, 
            ILogger<ImdbStatusBackgroundService> logger)
        {
            Services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Imdb Monitoring Service running.");

            await Monitor(stoppingToken);
        }

        private async Task Monitor(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Imdb Monitoring Service is monitoring ...");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IImdbMonitoringService>();

                await scopedProcessingService.CheckAvailability(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Imdb Monitoring Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
