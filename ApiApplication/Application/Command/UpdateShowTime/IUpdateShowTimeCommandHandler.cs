using ApiApplication.Application.Commands;
using ApiApplication.Core.CQRS;

namespace ApiApplication.Application.Command
{
    public interface IUpdateShowTimeCommandHandler : IAsyncCommandHandler<UpdateShowTimeRequest, UpdateShowTimeResponse>
    {
    }
}
