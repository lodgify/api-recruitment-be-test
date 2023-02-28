using ApiApplication.Application.Querie;
using FluentAssertions;
using Lodgify.Cinema.AcceptanceTest.Extensions;
using Lodgify.Cinema.AcceptanceTest.TestServers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Lodgify.Cinema.AcceptanceTest.StepDefinitions
{
    [Binding]
    public sealed class ShowTimeStepDefinitions
    {
        private  GetShowTimeRequest _getShowTimeRequest;
        private HttpResponseMessage _apiCallResult = null;

        [Given("i will not send filters")]
        public void i_will_not_send_filters()
        {
            _getShowTimeRequest = null;
        }

        [Given("i have the film (.*)")]
        public void i_will_not_send_filters(string movieTitle)
        {
            _getShowTimeRequest = new GetShowTimeRequest { MovieTitle = movieTitle };
        }
        

        [When("i call the api with the readonly token")]
        public void i_call_the_api_with_the_readonly_token()
        {
            var message = MoviesApiTestServer.Client.Get("Showtime", _getShowTimeRequest, token: MoviesApiTestServer.ReadOnlyToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }

        [Then("the result must be contains a list of show times")]
        public void the_result_must_be_contains_a_list_of_show_times()
        {
            _apiCallResult.EnsureSuccessStatusCode();
            var response = _apiCallResult.GetResult<IEnumerable<GetShowTimeResponse>>().Result;
            response.Should().NotBeNull();
        }



        [When("i call the api without the readonly token")]
        public void i_call_the_api_without_the_readonly_token()
        {
            var message = MoviesApiTestServer.Client.Get("Showtime", _getShowTimeRequest);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }

        [Then("the result must be contains a error (.*)")]
        public void the_result_must_be_contains_a_error(string error)
        {
            _apiCallResult.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }


        [Then("the result will be returned with the movie title (.*)")]
        public void the_result_will_be_returned_with_the_movie_title(string movieTitle)
        {
            _apiCallResult.EnsureSuccessStatusCode();
            var response = _apiCallResult.GetResult<IEnumerable<GetShowTimeResponse>>().Result;
            response.Should().NotBeNull();
            response.Should().NotBeNullOrEmpty();
            response.First().Movie.Title.Should().Be(movieTitle);
        }
    }
}