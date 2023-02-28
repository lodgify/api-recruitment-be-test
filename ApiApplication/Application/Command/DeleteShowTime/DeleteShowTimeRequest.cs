using ApiApplication.Core.CQRS;

namespace ApiApplication.Application.Command
{
    public sealed class DeleteShowTimeRequest : IRequest
    {
        public int Id { get; set; }
    }
}