using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using ApiApplication.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ApiApplication.Tasks
{
    public class ConsumeScopedServiceHostedService : BackgroundService

    {

        private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

        public ConsumeScopedServiceHostedService(IServiceProvider services,
            ILogger<ConsumeScopedServiceHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_logger.LogInformation(
            //    Constants.Log.TraceScopedHostServiceRunning);

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            //_logger.LogInformation(
            //   Constants.Log.TraceScopedHostServiceWorking);

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            //_logger.LogInformation(
            //   Constants.Log.TraceScopedHostServiceStopping);

            await base.StopAsync(stoppingToken);
        }
    }

}
