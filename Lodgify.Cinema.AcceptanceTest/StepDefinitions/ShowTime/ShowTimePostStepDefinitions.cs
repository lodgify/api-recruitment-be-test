using ApiApplication.Application.Command;
using FluentAssertions;
using Lodgify.Cinema.AcceptanceTest.Core;
using Lodgify.Cinema.AcceptanceTest.Extensions;
using Lodgify.Cinema.AcceptanceTest.TestServers;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Lodgify.Cinema.AcceptanceTest.StepDefinitions
{
    [Binding]
    public sealed class ShowTimeStepDefinitions
    {
        private AddShowTimeRequest _addShowTimeRequest;
        private AddShowTimeRequest _addShowTimeRequestErrorToken;
        private HttpResponseMessage _apiCallResult = null;

        [Given("i have this show time")]
        public void i_have_this_show_time(Table table)
        {
            _addShowTimeRequest = table.ToObject<AddShowTimeRequest>();
            _addShowTimeRequest.Schedule = new string[] { "10:00", "20:00" };

            _addShowTimeRequestErrorToken = table.ToObject<AddShowTimeRequest>();
            _addShowTimeRequestErrorToken.Schedule = new string[] { "10:00", "20:00" };
        }


        [When("i send the post data for the api")]
        public void i_send_the_post_data_for_the_api()
        {
            var message = MoviesApiTestServer.Client.Post("Showtime", _addShowTimeRequest, token: MoviesApiTestServer.WriteToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }

        [When("i send the post data for the api without token")]
        public void i_send_the_post_data_for_the_api_without_token()
        {
            var message = MoviesApiTestServer.Client.Post("Showtime", _addShowTimeRequestErrorToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }

        [When("i send the post data for the api with wrong token")]
        public void i_send_the_post_data_for_the_api_with_wrong_token()
        {
            var message = MoviesApiTestServer.Client.Post("Showtime", _addShowTimeRequestErrorToken, token: MoviesApiTestServer.ReadOnlyToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }


        [Then("the data will be saved and returned a success status code")]
        public void the_data_will_be_saved_and_returned_a_success_status_code()
        {
            _apiCallResult.EnsureSuccessStatusCode();
            _apiCallResult.IsSuccessStatusCode.Should().BeTrue();
        }

        [Then("the post result must be contains a 401 error")]
        public void the_post_result_must_be_contains_a_401_error()
        {
            _apiCallResult.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}