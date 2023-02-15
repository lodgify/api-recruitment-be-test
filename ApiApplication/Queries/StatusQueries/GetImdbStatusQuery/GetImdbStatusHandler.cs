using ApiApplication.Common;
using ApiApplication.Resources;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Queries.StatusQueries.GetImdbStatusQuery
{
    public class GetImdbStatusHandler : IRequestHandler<GetImdbStatusRequest, ImdbStatus>
    {
        private readonly IMapper _mapper;

        public GetImdbStatusHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ImdbStatus> Handle(GetImdbStatusRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ImdbStatus>(IMDBStatus.Instance);
        }
    }
}
