using ApiApplication.Database.Entities;
using FlowerSpot.IntegrationTests.Repositories.Base;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FlowerSpot.IntegrationTests.Repositories
{
    public class RepositoryTest : RepositoryTestFixture
    {
        private readonly ShowtimeEntity ShowtimeEntity = new ShowtimeEntity()
        {
            Id = new Random().Next(1, 10000),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(3),
            AuditoriumId = 1,
            Schedule = new List<string> { "17:00", "18:00" },
            Movie = new MovieEntity()
            {
                Title = "Movie 1",
                ImdbId = "ttt1110",
                Stars = "S1, S2, S3",
                ReleaseDate = DateTime.Now,

            }
        };

        [Fact]
        public void Add_Sighting_ReturnsAddedEntity()
        {
            #region Arrange

            var showtimesRepository = GetShowtimeRepository();

            #endregion

            #region Act

            var response = showtimesRepository.Add(ShowtimeEntity);

            #endregion

            #region Assert

            response.Should().BeOfType<ShowtimeEntity>();

            #endregion
        }

        [Fact]
        public void GetByIdAsync_Sighting_ReturnsEntity()
        {
            #region Arrange
            var showtimesRepository = GetShowtimeRepository();

            var entity = showtimesRepository.Add(ShowtimeEntity);

            #endregion

            #region Act

            var response = showtimesRepository.GetById(entity.Id);

            #endregion

            #region Assert

            response.Should().BeOfType<ShowtimeEntity>();

            #endregion
        }

        [Theory]
        [InlineData(9999)]
        public void GetByInvalidIdAsync_Sighting_ReturnsNull(int id)
        {
            #region Arrange
            var showtimesRepository = GetShowtimeRepository();

            #endregion

            #region Act

            var response = showtimesRepository.GetById(id);

            #endregion

            #region Assert

            response.Should().BeNull();

            #endregion
        }


        [Fact]
        public void Delete_Sighting_ReturnsTrue()
        {
            #region Arrange
            var showtimesRepository = GetShowtimeRepository();

            var entity = showtimesRepository.Add(ShowtimeEntity);

            #endregion

            #region Act

            var response = showtimesRepository.Delete(entity.Id);

            #endregion

            #region Assert

            response.Should().BeOfType<ShowtimeEntity>();

            #endregion
        }

        //TODO: Test other methods of repository in same manner
    }
}
