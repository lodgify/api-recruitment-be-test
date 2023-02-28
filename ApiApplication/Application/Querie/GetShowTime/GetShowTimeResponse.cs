using ApiApplication.Application.Command;
using ApiApplication.Core.CQRS;

namespace ApiApplication.Application.Querie
{
    public sealed class GetShowTimeResponse : AddShowTimeDto, IResponse
    {
        public int Id { get; set; }
    }
}
