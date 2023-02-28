using ApiApplication.Core.CQRS;
using System.Collections.Generic;

namespace ApiApplication.Application.Querie
{
    public interface IGetShowTimeQueryHandler : IAsyncQueryHandler<GetShowTimeRequest, IEnumerable<GetShowTimeResponse>>
    {
    }
}
