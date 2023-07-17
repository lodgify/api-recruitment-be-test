using ApiApplication.Database.Entities;
using ApiApplication.Models;

namespace ApiApplication.Mappings
{
    public static class MovieMappings
    {
        public static Movie MapToResponse(this MovieEntity entity)
        {
            return new Movie
            {
                Title = entity.Title,
                ImdbId = entity.ImdbId,
                Stars = entity.Stars,
                ReleaseDate = entity.ReleaseDate
            };
        }
    }
}