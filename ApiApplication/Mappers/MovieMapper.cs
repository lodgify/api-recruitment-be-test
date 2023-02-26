using ApiApplication.Database.Entities;
using ApiApplication.Services;

namespace ApiApplication.Mappers
{
    public static class MovieMapper
    {
        public static MovieEntity MapToEntity(TitleImdbEntity titleImdbEntity)
        {
            return new MovieEntity()
            {
                ImdbId  = titleImdbEntity.Id,
                Title = titleImdbEntity.Title,
                ReleaseDate = titleImdbEntity.ReleaseDate,
                Stars = titleImdbEntity.Stars
            };
        }
    }
}
