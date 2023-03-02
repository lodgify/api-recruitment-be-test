using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Core.Worker
{
    public class ImdbHealtCheckWebWorker : BackgroundService
    {
        private const int ONE_MINUTE = 60000;
        private readonly IImdbStatus _imdbStatus;
        private readonly IServiceCollection _serviceCollection;

        public ImdbHealtCheckWebWorker(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _imdbStatus = _serviceCollection.BuildServiceProvider().GetService<IImdbStatus>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var serviceProvider = _serviceCollection.BuildServiceProvider();
                    var _imdbStatusService = serviceProvider.GetService<IImdbStatusService>();
                    var _logService = serviceProvider.GetService<ILodgifyLogService>();

                    var status = await _imdbStatusService.IsUpAsync(cancellationToken);
                    _imdbStatus.SetCheck(status.IsUp, DateTime.Now);
                    _logService.Log($"ImdbHealtCheckWebWorker running - {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    _imdbStatus.SetException(ex, DateTime.Now);
                }

                await Task.Delay(ONE_MINUTE, cancellationToken);
            }
        }
    }
}