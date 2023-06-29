using ApiApplication.Models;
using ApiApplication.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace ApiApplication.Services
{
    public class ServiceImdbStatus : IHostedService, IDisposable    
    {
        private readonly IServiceImdbApi _serviceImdbApi;
        private readonly ImdbStatus _imdbStatus;
        private readonly Timer _statusUpdateTimer;
        public ServiceImdbStatus(IServiceImdbApi serviceImdbApi, ImdbStatus imdbStatus, IConfiguration configuration)
        {
            var _pingInterval = configuration.GetSection("ImdbService:PingInterval").Get<int>();
            _serviceImdbApi = serviceImdbApi;
            _imdbStatus = imdbStatus;            
            _statusUpdateTimer = new Timer(UpdateStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(_pingInterval));

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _statusUpdateTimer?.Dispose();
            return Task.CompletedTask;
        }
        private async void UpdateStatus(object state)
        {
            _imdbStatus.LastCall = DateTime.Now;
            _imdbStatus.Up = await _serviceImdbApi.GetApiStatus();            
        }

        public void Dispose() => _statusUpdateTimer?.Dispose();
    }
}
