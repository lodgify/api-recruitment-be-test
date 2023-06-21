using ApiApplication.ImdbApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Workers
{
    public class ImdbStatusChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private const int _millisecondsDelay = 60000;
        

        public ImdbStatusChecker(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var imdbApiClient = _serviceProvider.GetRequiredService<IImdbApiClient>();
            var statusInfo = _serviceProvider.GetRequiredService<IStatusInfo>();            

            while (!stoppingToken.IsCancellationRequested)
            {
                HttpStatusCode statusId = await imdbApiClient.GetStatus();

                statusInfo.SetStatus(statusId);

                await Task.Delay(_millisecondsDelay, stoppingToken);
            }
        }
    }
}
