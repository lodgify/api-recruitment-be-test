using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Core.CQRS
{
    public interface IAsyncQueryHandler<IRequest, IResponse>
    {
        public Task<IResponse> ExecuteGetAsync(IRequest request, CancellationToken cancellationToken);
    }

    public interface IAsyncQueryHandler<IResponse>
    {
        public Task<IResponse> ExecuteGetAsync(CancellationToken cancellationToken);
    }
}
