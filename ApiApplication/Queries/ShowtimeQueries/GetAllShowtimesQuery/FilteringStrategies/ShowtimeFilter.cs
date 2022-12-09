using ApiApplication.Resources;
using System.Collections.Generic;

namespace ApiApplication.Queries.ShowtimeQueries.GetAllShowtimesQuery.FilteringStrategies
{
    public abstract class ShowtimeFilter
    {
        public abstract IEnumerable<Showtime> GetShowtimes(GetAllShowtimesRequest request);
    }
}
