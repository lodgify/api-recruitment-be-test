using ApiApplication.Core.CQRS;
using System;

namespace ApiApplication.Application.Querie
{
    public sealed class GetShowTimeRequest: IRequest
    {
        public string MovieTitle { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
