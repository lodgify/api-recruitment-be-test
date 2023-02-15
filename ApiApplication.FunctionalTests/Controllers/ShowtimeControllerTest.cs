using ApiApplication.Models;
using ApiApplication.Models.Movies;
using ApiApplication.Models.Showtimes;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ApiApplication.FunctionalTests.Controllers
{
    public class ShowtimeControllerTest : FunctionalTestStartup
    {
        [Fact]
        public async Task AddShowtimeUnauthorized_ReturnsUnauthorized()
        {
            #region Arrange

            var url = $"api/showtime";

            var showtime = new AddShowtimeModel
            {
                StartDate = DateTime.Now,
                Schedule = new List<string> { "17:00", "18:00" },
                EndDate = DateTime.Now,
                Movie = new AddMovieModel
                {
                    ImdbId = "tt0111161"
                }
            };

            #endregion

            #region Act

            var response = await PostAsync(url, showtime);

            #endregion

            #region Assert

            response?.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            #endregion
        }

        [Fact]
        public async Task AddShowtime_WithouToken_ReturnsUnauthorized()
        {
            #region Arrange

            var url = $"api/showtime";

            var showtime = new AddShowtimeModel
            {
                StartDate = DateTime.Now,
                Schedule = new List<string> { "17:00", "18:00" },
                EndDate = DateTime.Now,
                Movie = new AddMovieModel
                {
                    ImdbId = "tt0111161"
                }
            };

            #endregion

            #region Act

            var responseMessage = await PostAsync(url, showtime);

            var response = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseModel<ShowtimeModel>>(response);

            #endregion

            #region Assert

            result?.Success.Should().Be(false);
            result?.StatusCode.Should().Be(401);
            result?.Result.Should().NotBeNull();

            #endregion
        }

        [Fact]
        public async Task AddShowtimeAuthorized_ReturnsNewShowtime()
        {
            #region Arrange

            WriteTokenAuthenticate();

            var url = $"api/showtime";

            var showtime = new AddShowtimeModel
            {

                StartDate = DateTime.Now,
                Schedule = new List<string> { "17:00", "18:00" },
                EndDate = DateTime.Now,
                Movie = new AddMovieModel
                {
                    ImdbId = "tt0111161"
                }
            };

            #endregion

            #region Act

            var responseMessage = await PostAsync(url, showtime);

            var response = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseModel<ShowtimeModel>>(response);

            #endregion

            #region Assert

            responseMessage?.StatusCode.Should().Be(HttpStatusCode.OK);
            result?.Success.Should().Be(true);
            result?.Result.Should().NotBeNull();

            #endregion
        }


        [Fact]
        public async Task GetAll_WithouReadToken_ReturnsUnauthorized()
        {
            #region Arrange

            var url = "api/showtime/";

            #endregion

            #region Act

            var responseMessage = await GetAsync(url);
            var response = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<List<ShowtimeModel>>>(response);

            #endregion

            #region Assert

            result?.Success.Should().Be(false);
            result?.StatusCode.Should().Be(401);
            result?.Result.Should().NotBeNull();

            #endregion
        }

        [Fact]
        public async Task GetAll_ReturnsListOfShowtimes()
        {
            #region Arrange

            var url = "api/showtime/";

            ReadTokenAuthenticate();

            #endregion

            #region Act

            var responseMessage = await GetAsync(url);
            var response = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel<List<ShowtimeModel>>>(response);

            #endregion

            #region Assert

            result?.Success.Should().Be(true);
            result?.StatusCode.Should().Be(200);
            result?.Result.Should().NotBeNull();

            #endregion
        }
    }

}
