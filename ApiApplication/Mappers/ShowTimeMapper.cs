using ApiApplication.Database.Entities;
using ApiApplication.Models;

namespace ApiApplication.Mappers
{
    public static class ShowTimeMapper
    {
        public static ShowtimeEntity MapToEntity(ShowTimeRequestModel showTimeModel)
        {
            return new ShowtimeEntity()
            {
                Id = showTimeModel.Id,
                AuditoriumId = showTimeModel.AuditoriumId,
                StartDate = showTimeModel.StartDate,
                EndDate = showTimeModel.EndDate,
                Movie = new MovieEntity()
                {
                    Id = showTimeModel.Movie.Id,
                    ImdbId = showTimeModel.Movie.ImdbId,
                    ReleaseDate = showTimeModel.Movie.ReleaseDate,
                    ShowtimeId = showTimeModel.Movie.Id,
                    Stars = showTimeModel.Movie.Stars,
                    Title = showTimeModel.Movie.Title
                }
            };
        }

        public static ShowtimeEntity MapToEntity(ShowTimeRequestModel showTimeModel, MovieEntity movieEntity)
        {
            return new ShowtimeEntity()
            {
                Id = showTimeModel.Id,
                AuditoriumId = showTimeModel.AuditoriumId,
                StartDate = showTimeModel.StartDate,
                EndDate = showTimeModel.EndDate,
                Movie = new MovieEntity()
                {
                    Id = showTimeModel.Movie.Id,
                    ImdbId = showTimeModel.Movie.ImdbId,
                    ReleaseDate = movieEntity.ReleaseDate,
                    ShowtimeId = showTimeModel.Id,
                    Stars = movieEntity.Stars,
                    Title = movieEntity.Title
                }
            };
        }


        public static ShowTimeResponseModel MapToModel(ShowtimeEntity showTimeEntity)
        {
            return new ShowTimeResponseModel()
            {
                Id = showTimeEntity.Id,
                AuditoriumId = showTimeEntity.AuditoriumId,
                StartDate = showTimeEntity.StartDate,
                EndDate = showTimeEntity.EndDate,
                Movie = new MovieResponseModel()
                {
                    Id = showTimeEntity.Movie.Id,
                    ImdbId = showTimeEntity.Movie.ImdbId,
                    ReleaseDate = showTimeEntity.Movie.ReleaseDate,
                    ShowtimeId = showTimeEntity.Movie.ShowtimeId,
                    Stars = showTimeEntity.Movie.Stars,
                    Title = showTimeEntity.Movie.Title
                }
            };
        }

    }
}
