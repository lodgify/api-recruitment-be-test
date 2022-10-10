using ApiApplication.ImdbService.Service;
using ApiApplication.ImdbService.Service.Implementors;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundServices
{
    public class ImdbStatusCheckerService : BackgroundService
    {
        private readonly IImdbService imdbService;
        public ImdbStatusCheckerService(IImdbService imdbService) {
            this.imdbService = imdbService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested)
            {
                var newMoviesData = await imdbService.FetchCommingSoon();
                bool imdbServiceStatus = newMoviesData.ErrorMessage != null;
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
