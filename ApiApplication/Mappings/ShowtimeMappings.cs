using System.Collections.Generic;
using System.Linq;
using ApiApplication.Database.Entities;
using ApiApplication.Models.Showtime;

namespace ApiApplication.Mappings
{
    public static class ShowtimeMappings
    {
        public static IEnumerable<Showtime> MapToResponse(this IEnumerable<ShowtimeEntity> entities)
        {
            return entities.Select(entity => entity.MapToResponse());
        }

        public static Showtime MapToResponse(this ShowtimeEntity entity)
        {
            return new Showtime
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Schedule = entity.Schedule,
                Movie = entity.Movie.MapToResponse(),
                AuditoriumId = entity.AuditoriumId
            };
        }
    }
}