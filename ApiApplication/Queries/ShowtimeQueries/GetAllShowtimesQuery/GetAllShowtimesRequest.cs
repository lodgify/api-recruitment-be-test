using ApiApplication.Resources;
using MediatR;
using System;
using System.Collections.Generic;

namespace ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery
{
    public class GetAllShowtimesRequest : IRequest<IEnumerable<Showtime>>
    {
        public DateTime? Date { get; set; }

        public string Title { get; set; }
    }
}
