using Cinema.Entities.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Tests
{
    public class Tests
    {

        HttpClient client;

        public Tests()
        {
            client = new TestClientProvider().Client;
        }

        [SetUp]
        public void Setup()
        {
            client.DefaultRequestHeaders.Clear();

        }

        [Test, Order(1)]
        public async Task Status_Call_Returns_Success_Without_Authentication()
        {

            var httpResponse = await client.GetAsync("api/Status");
            Assert.True(httpResponse.IsSuccessStatusCode);

        }

        [Test, Order(2)]
        public async Task GetShowtimes_UnAuthenticated_Returns_Unauthorized()
        {
            var httpResponse = await client.GetAsync("api/Showtime/v1");

            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }

        [Test, Order(3)]
        public async Task GetShowtimes_Authenticated_Using_Write_Token_Returns_Forbidden()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            var httpResponse = await client.GetAsync("api/Showtime/v1");

            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [Test, Order(4)]
        public async Task GetShowtimes_Authenticated_Using_Read_Token_Returns_Success()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "MTIzNHxSZWFk");
            var httpResponse = await client.GetAsync("api/Showtime/v1");

            Assert.True(httpResponse.IsSuccessStatusCode);

        }

        [Test, Order(5)]
        public async Task Add_New_Showtime_Should_Return_Success()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 1,
                StartDate = new DateTime(2022, 5, 1),
                EndDate = new DateTime(2022, 7, 1),
                Movie = new MovieDto() { ImdbId = "tt0438231" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/Showtime/v1", httpContent);

            Assert.True(httpResponse.IsSuccessStatusCode);

        }

        [Test, Order(6)]
        public async Task Update_Showtime_With_Overlapping_Dates_Should_Return_Badrequest()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 1,
                StartDate = new DateTime(2022, 4, 1),
                EndDate = new DateTime(2022, 6, 1),
                Movie = new MovieDto() { ImdbId = "tt0108052" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/Showtime/v1?id=2", httpContent);


            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Test, Order(7)]
        public async Task Update_Added_Showtime_Change_Auditorium_Id_Should_Return_Success()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 2,
                StartDate = DateTime.Now.AddDays(60),
                EndDate = DateTime.Now.AddDays(90),
                Movie = new MovieDto() { ImdbId = "tt0108052" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/Showtime/v1?id=2", httpContent);

            Assert.True(httpResponse.IsSuccessStatusCode);

        }

        [Test, Order(8)]
        public async Task Add_New_Showtime_With_Dates_Overlap_Should_Return_BadRequest()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Movie = new MovieDto() { ImdbId = "tt0468569" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/Showtime/v1", httpContent);

            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Test, Order(9)]
        public async Task Add_New_Showtime_With_Non_Existing_Auditorium_Should_Return_BadRequest()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Movie = new MovieDto() { ImdbId = "tt0468569" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/Showtime/v1", httpContent);

            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Test, Order(10)]
        public async Task Update_Showtime_Change_Auditorium_Id_To_Non_Existing_Auditorium_Should_Return_BadRequest()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");
            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 10,
                StartDate = DateTime.Now.AddDays(60),
                EndDate = DateTime.Now.AddDays(90),
                Movie = new MovieDto() { ImdbId = "tt0108052" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/Showtime/v1?id=2", httpContent);

            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Test, Order(11)]
        public async Task Delete_Showtime_Should_Return_Success()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");

            var httpResponse = await client.DeleteAsync("api/Showtime/v1?id=1");

            Assert.True(httpResponse.IsSuccessStatusCode);

        }

        [Test, Order(12)]
        public async Task Patch_Request_Should_Return_InternalServerError()
        {
            client.DefaultRequestHeaders.Add("ApiKey", "Nzg5NHxXcml0ZQ==");

            ShowtimeDto showTime = new ShowtimeDto()
            {
                AuditoriumId = 2,
                StartDate = DateTime.Now.AddDays(60),
                EndDate = DateTime.Now.AddDays(90),
                Movie = new MovieDto() { ImdbId = "tt0108052" },
                Schedule = new string[] { "12:00", "13:00" }
            };
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            string json = JsonConvert.SerializeObject(showTime, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await client.PatchAsync("api/Showtime/v1", httpContent);

            Assert.True(httpResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError);

        }





    }
}