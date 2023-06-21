using ApiApplication.Database.Entities;
using ApiApplication.ImdbApi.Models;
using ApiApplication.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiApplication.Tests.Infrastructure
{
    [Collection(nameof(TestCollection))]
    public class ShowtimeTests : IntegrationTestsBase
    {
        public ShowtimeTests([NotNull] TestApplicationFactory<Program> applicationFactory) : base(applicationFactory)
        {

        }

        [Fact]
        public async Task ShouldGetShowtimesWhenTheyArePresent()
        {
            var showtimeEntity = new ShowtimeEntity
            {
                Id = 100500,
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 4, 1),
                Movie = new MovieEntity
                {
                    Id = 100501,
                    Title = "Inception",
                    ImdbId = "tt1375666",
                    ReleaseDate = new DateTime(2010, 01, 14),
                    Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
                },
                Schedule = new List<string> { "16:00", "17:00", "18:00", "18:30", "19:00", "22:00" },
                AuditoriumId = 1
            };

            Context.Showtimes.Add(showtimeEntity);
            Context.SaveChanges();

            Client.DefaultRequestHeaders.Add("ApiKey", "MTIzNHxSZWFk");
            HttpResponseMessage response = await Client.GetAsync("api/showtime");

            string responseBody = await response.Content.ReadAsStringAsync();
            List<Showtime> showtimes = JsonConvert.DeserializeObject<List<Showtime>>(responseBody);

            Assert.True(showtimes.Any());
        }

        [Fact]
        public async Task ShouldThrowErrorWhenAuthorizationFailed()
        {
            Client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            HttpResponseMessage response = await Client.GetAsync("api/showtime");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenAuthenticationFailed()
        {
            Client.DefaultRequestHeaders.Add("ApiKey", "WRONGKEY");
            HttpResponseMessage response = await Client.GetAsync("api/showtime");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ShouldCreateNewShowtime()
        {
            Client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");

            string jsonData = @"{
        ""start_date"": ""2022-01-01T00:00:00"",
        ""end_date"": ""2022-04-01T00:00:00"",
        ""schedule"": ""16:00,17:00,18:00,18:30,19:00,22:00"",
        ""movie"": {
            ""imdb_id"": ""tt1375666""
        },
        ""auditorium_id"": 1
    }";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync("api/showtime", content);

            string responseBody = await response.Content.ReadAsStringAsync();
            Showtime showtime = JsonConvert.DeserializeObject<Showtime>(responseBody, new JsonSerializerSettings()
            {
                ContractResolver = new UnderscorePropertyNamesContractResolver()
            });

            Assert.True(showtime.Id > 0);
            Assert.Equal("tt1375666", showtime.Movie.ImdbId);
        }

        [Fact]
        public async Task ShouldUpdateExistingShowtimeWhenItWasFound()
        {
            var showtimeEntity = new ShowtimeEntity
            {
                Id = 100600,
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 4, 1),
                Movie = new MovieEntity
                {
                    Id = 100601,
                    Title = "Inception",
                    ImdbId = "tt1375666",
                    ReleaseDate = new DateTime(2010, 01, 14),
                    Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
                },
                Schedule = new List<string> { "16:00", "17:00", "18:00", "18:30", "19:00", "22:00" },
                AuditoriumId = 1
            };

            Context.Showtimes.Add(showtimeEntity);
            Context.SaveChanges();

            Client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");

            string jsonData = @"    {
    	""id"": 100600,
        ""start_date"": ""2023-01-01T00:00:00"",
        ""end_date"": ""2023-04-01T00:00:00"",
        ""schedule"": ""16:00,17:00,18:00,18:30,19:00,22:00"",
        ""movie"": {
            ""imdb_id"": ""tt1375666""
        },
        ""auditorium_id"": 1
    }";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PutAsync("api/showtime", content);

            string responseBody = await response.Content.ReadAsStringAsync();
            Showtime showtime = JsonConvert.DeserializeObject<Showtime>(responseBody, new JsonSerializerSettings()
            {
                ContractResolver = new UnderscorePropertyNamesContractResolver()
            });

            Assert.True(showtime.Id == 100600);
            Assert.Equal(new DateTime(2023, 1, 1), showtime.StartDate);
        }

        [Fact]
        public async Task ShouldDeleteExistingShowtimeWhenItWasFound()
        {
            Client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");

            HttpResponseMessage response = await Client.DeleteAsync("api/showtime?id=1");

            string responseBody = await response.Content.ReadAsStringAsync();
            Showtime showtime = JsonConvert.DeserializeObject<Showtime>(responseBody, new JsonSerializerSettings()
            {
                ContractResolver = new UnderscorePropertyNamesContractResolver()
            });

            Assert.True(showtime.Id == 1);

            Client.DefaultRequestHeaders.Remove("ApiKey");

            // Verifying that the record was really deleted
            Client.DefaultRequestHeaders.Add("ApiKey", "MTIzNHxSZWFk");
            response = await Client.GetAsync("api/showtime");
            responseBody = await response.Content.ReadAsStringAsync();
            List<Showtime> showtimes = JsonConvert.DeserializeObject<List<Showtime>>(responseBody);
            Assert.Empty(showtimes.Where(s => s.Id == 1)); // 0 showtimes should be found
        }
    }
}
