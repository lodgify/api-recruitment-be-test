using ApiApplication.Database.Entities;

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
//                ShowtimeId = titleImdbEntity.ShowtimeId,
                Stars = titleImdbEntity.Stars
            };
        }
    }
}
