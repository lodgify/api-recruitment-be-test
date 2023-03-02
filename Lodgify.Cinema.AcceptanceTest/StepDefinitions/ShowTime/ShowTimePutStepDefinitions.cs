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
    public sealed class ShowTimePutStepDefinitions
    {
        private UpdateShowTimeRequest _updateShowTimeRequest;
        private HttpResponseMessage _apiCallResult = null;

        [Given("i have this show time to update")]
        public void i_have_this_show_time_to_update(Table table)
        {
            _updateShowTimeRequest = table.ToObject<UpdateShowTimeRequest>();
        }


        [When("i send the put data for the api")]
        public void i_send_the_put_data_for_the_api()
        {
            var message = MoviesApiTestServer.Client.Put("Showtime", _updateShowTimeRequest, token: MoviesApiTestServer.WriteToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }


        [Then("the data must be updated and will returned a success status code")]
        public void the_data_must_be_updated_and_will_returned_a_success_status_code()
        {
            _apiCallResult.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}