using ApiApplication.Resources;
using MediatR;
using System.Collections.Generic;

namespace ApiApplication.Queries.StatusQueries.GetImdbStatusQuery
{
    public class GetImdbStatusRequest : IRequest<ImdbStatus>
    {
    }
}
