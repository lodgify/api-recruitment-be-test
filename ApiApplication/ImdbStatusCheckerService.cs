using ApiApplication.ImdbService.Service;
using ApiApplication.ImdbService.Service.Implementors;
using ApiApplication.Services;
using ApiApplication.Services.Implementors;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication
{
    public class ImdbStatusCheckerService : BackgroundService
    {
        private readonly IImdbService imdbService;
        private readonly IImdbApiStatusService imdbApiStatusService;
        public ImdbStatusCheckerService(IImdbService imdbService, IImdbApiStatusService imdbApiStatusService) {
            this.imdbService = imdbService;
            this.imdbApiStatusService = imdbApiStatusService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var newMoviesData = await imdbService.FetchCommingSoon();
                imdbApiStatusService.imdbStatus.LastCall = DateTime.Now;
                imdbApiStatusService.imdbStatus.Up = string.IsNullOrEmpty(newMoviesData.ErrorMessage);
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
