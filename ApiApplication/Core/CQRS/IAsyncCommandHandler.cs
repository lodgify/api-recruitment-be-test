using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Core.CQRS
{
    public interface IAsyncCommandHandler<ICommand>
    {
        public Task ExecuteAsync(ICommand command);
    }

    public interface IAsyncCommandHandler<ICommand, IResponse>
    {
        public Task<IResponse> ExecuteAsync(ICommand command, CancellationToken cancellationToken);
    }

    public interface IGenericAsyncCommandHandler<ICommand, IResponse>
    {
        public Task<IResponse> ExecuteAsync<T>(ICommand command, CancellationToken cancellationToken) where T : class;
    }
}
