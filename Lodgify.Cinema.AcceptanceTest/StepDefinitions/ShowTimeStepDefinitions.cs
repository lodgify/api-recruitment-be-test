using ApiApplication.Application.Querie;
using Lodgify.Cinema.AcceptanceTest.TestServers;
using System.Net.Http;
using TechTalk.SpecFlow;
using Lodgify.Cinema.AcceptanceTest.Extensions;
using System.Collections.Generic;
using FluentAssertions;

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
        
    }
}