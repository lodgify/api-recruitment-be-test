using ApiApplication.Application.Command;
using ApiApplication.Application.Querie;
using FluentAssertions;
using Lodgify.Cinema.AcceptanceTest.Extensions;
using Lodgify.Cinema.AcceptanceTest.TestServers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Lodgify.Cinema.AcceptanceTest.StepDefinitions
{
    [Binding]
    public sealed class ShowTimeDeleteStepDefinitions
    {
        private DeleteShowTimeRequest _deleteShowTimeRequest;
        private HttpResponseMessage _apiCallResult = null;
        private readonly int _imdbId = 1375640;

        [Given("i have a show time id with id number (.*)")]
        public void i_have_a_show_time_id_with_id_number(int id)
        {
            _deleteShowTimeRequest = new DeleteShowTimeRequest { Id = id };
        }

        [When("i call the api to delete the record")]
        public void i_call_the_api_to_delete_the_record()
        {
            InsertNewShowTimeWithIdTwo();
            var message = MoviesApiTestServer.Client.Delete("Showtime", _deleteShowTimeRequest, token: MoviesApiTestServer.WriteToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }

        private void InsertNewShowTimeWithIdTwo()
        {
            AddShowTimeRequest addShowTimeRequest = new AddShowTimeRequest
            {
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now.AddDays(-1),
                Schedule = new string[] { "14:00" },
                Imdb_id = _imdbId
            };

            var message = MoviesApiTestServer.Client.Post("Showtime", addShowTimeRequest, token: MoviesApiTestServer.WriteToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
            _apiCallResult.EnsureSuccessStatusCode();
        }

        [Then("the result shold be a success status for delete")]
        public void the_result_shold_be_a_success_status_for_delete()
        {
            _apiCallResult.IsSuccessStatusCode.Should().BeTrue();
        }

        [Then("i need to call a get route to confirm the deletion of this record")]
        public void i_need_to_call_a_get_route_to_confirm_the_deletion_of_this_record()
        {
            var message = MoviesApiTestServer.Client.Get("Showtime", null, token: MoviesApiTestServer.ReadOnlyToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
            var response = _apiCallResult.GetResult<IEnumerable<GetShowTimeResponse>>().Result;
            response.Any(w => w.Id == _deleteShowTimeRequest.Id).Should().BeFalse();
        }
    }
}