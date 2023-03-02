using ApiApplication.Core.CQRS;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.Command
{
    public sealed class UpdateShowTimeRequest : IRequest
    {
        public int Id { get; set; }
        public int? Imdb_id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<string> Schedule { get; set; }
        public int? AuditoriumId { get; set; }
    }
}