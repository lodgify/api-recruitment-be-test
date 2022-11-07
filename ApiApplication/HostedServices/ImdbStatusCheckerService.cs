using System;
using System.Threading;
using System.Threading.Tasks;

using ApiApplication.DTO;
using ApiApplication.Models;
using ApiApplication.Services;

using Microsoft.Extensions.Hosting;

namespace ApiApplication.HostedServices {
    public class ImdbStatusCheckerService : IHostedService, IDisposable {
        private readonly IImdbService _imdbService;
        private readonly ImdbStatus _imdbStatus;
        private Timer _timer;
        private const string LeonImdbId = "tt0110413";

        public ImdbStatusCheckerService(IImdbService imdbService,
                                        ImdbStatus imdbStatus) {
            _imdbService = imdbService;
            _imdbStatus = imdbStatus;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _timer = new Timer(CheckStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        #region Private Methods
        private void CheckStatus(object? state) {
            Result checkResult = _imdbService.GetAsync(LeonImdbId).Result;
            _imdbStatus.LastCall = DateTime.Now;
            _imdbStatus.Up = checkResult.Success;
        }
        #endregion

        #region IDisposable Members
        public void Dispose() {
            _timer.Dispose();
        }
        #endregion
    }
}
