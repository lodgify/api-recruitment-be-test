using ApiApplication.Controllers;
using ApiApplication.Models;
using ApiApplication.Models.Movies;
using ApiApplication.Models.Showtimes;
using ApiApplication.Services.Showtimes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiApplication.UnitTests.Controllers
{
    public class ShowtimeControllerTests
    {
        private readonly Mock<IShowtimeService> _mockService;
        private readonly ShowtimeController _controller;

        public ShowtimeControllerTests()
        {
            _mockService = new Mock<IShowtimeService>();
            _controller = new ShowtimeController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ActionExecutes_ReturnsExactNumberOfFlowers()
        {

            #region Arrange
            var model = new ResponseModel<IList<ShowtimeModel>>()
            {
                Result = new List<ShowtimeModel>()
                {
                    new ShowtimeModel(){
                        Id=1,
                        Schedule=new List<string> { "17:00", "18:00"},
                         AuditoriumId=1,
                         EndDate=DateTime.Now.AddDays(3),
                         StartDate=DateTime.Now,
                         Movie=new MovieModel
                         {
                             ImdbId="ttt1110",
                             Stars="Icer Cassilas, Xavi, Iniesta, Ramos",
                             ReleaseDate=DateTime.Now,
                             Title="Spain football team",
                         }
                    }
                },
                StatusCode = 200,
            };

            _mockService.Setup(service => service.GetAll(null, DateTime.MinValue))
                .Returns(model);
            #endregion

            #region Act
            var result = _controller.GetAll(null, DateTime.MinValue);
            #endregion


            #region Asserts

            var showtimes = ((ObjectResult)result).Value as ResponseModel<IList<ShowtimeModel>>;

            Assert.Equal(1, showtimes.Result.Count);
            showtimes.StatusCode.Should().Be(200);


            #endregion
        }

        [Theory]
        [InlineData(-1)]
        public void GetById_InvalidIdReturnNotFound(int id)
        {
            #region Arrage
            _mockService.Setup(service => service.GetById(id))
               .Returns(new ResponseModel<ShowtimeModel>()
               {
                   StatusCode = 404,
                   Errors = new List<string>() { "This entity does not exist" },
               });

            #endregion

            #region Act
            var result = _controller.GetById(id);

            #endregion

            #region Assert
            var showtime = ((ObjectResult)result).Value as ResponseModel<ShowtimeModel>;

            showtime.Success.Should().Be(false);
            showtime.Result.Should().Be(null);
            showtime.StatusCode.Should().Be(404);
            #endregion
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ValidIdReturnResult(int id)
        {
            #region Arrage
            _mockService.Setup(service => service.GetById(id))
               .Returns(new ResponseModel<ShowtimeModel>()
               {
                   StatusCode = 200,
                   Result = new ShowtimeModel()
                   {
                       Id = id,
                   }
               });

            #endregion

            #region Act
            var result = _controller.GetById(id);

            #endregion

            #region Assert
            var showtime = ((ObjectResult)result).Value as ResponseModel<ShowtimeModel>;

            showtime.Success.Should().Be(true);
            showtime.Result.Should().BeOfType<ShowtimeModel>();
            showtime.StatusCode.Should().Be(200);
            #endregion
        }

        [Fact]
        public void Add_ModelStateValid_ReturnsFlowerModel()
        {
            #region Arrange

            var showtimeModel = new AddShowtimeModel
            {
                Schedule = new List<string> { "17:00", "18:00" },
                AuditoriumId = 1,
                EndDate = DateTime.Now.AddDays(3),
                StartDate = DateTime.Now,
                Movie = new AddMovieModel
                {
                    ImdbId = "ttt1110",

                }
            };
            _mockService.Setup(service => service.Add(showtimeModel))
                    .Returns(new ResponseModel<ShowtimeModel>()
                    {
                        StatusCode = 200,
                        Result = new ShowtimeModel()
                        {
                            Id = 1,

                        }
                    });
            #endregion

            #region Act
            var result = _controller.Add(showtimeModel);
            #endregion

            #region Assert
            var showtime = ((ObjectResult)result).Value as ResponseModel<ShowtimeModel>;

            showtime.StatusCode.Should().Be(200);
            showtime.Errors.Should().BeNullOrEmpty();
            showtime.Success.Should().Be(true);
            showtime.Result.Should().BeOfType<ShowtimeModel>();
            #endregion
        }


        [Theory]
        [InlineData(1)]
        public void DeleteById_ValidIdReturnResult(int id)
        {
            #region Arrage
            _mockService.Setup(service => service.Delete(id))
               .Returns(new ResponseModel<bool>()
               {
                   StatusCode = 200,
                   Result = true
               });

            #endregion

            #region Act
            var result = _controller.Delete(id);

            #endregion

            #region Assert
            var showtime = ((ObjectResult)result).Value as ResponseModel<bool>;

            showtime.Success.Should().Be(true);
            showtime.StatusCode.Should().Be(200);

            #endregion
        }
    }

}
