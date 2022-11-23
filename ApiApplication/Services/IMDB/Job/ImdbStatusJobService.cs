using ApiApplication.Services.IMDB.Status;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services.IMDB.Job
{
    public class ImdbStatusJobService : BackgroundService
    {
        private Timer _timer;

        private readonly IImdbService _imdbService;
        private readonly ImdbStatusService _imdbStatusService;

        public ImdbStatusJobService(IImdbService imdbService,
                                    ImdbStatusService imdbStatusService)
        {
            _imdbService = imdbService;
            _imdbStatusService = imdbStatusService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(Dowork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        }

        private void Dowork(object? state)
        {
            _imdbStatusService.ImdbStatus.LastCall = DateTime.Now;
            _imdbStatusService.ImdbStatus.Up = _imdbService.CheckApiStatus().Result;
        }
    }
}
