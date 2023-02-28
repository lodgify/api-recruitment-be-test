using Lodgify.Cinema.AcceptanceTest.Extensions;
using Lodgify.Cinema.AcceptanceTest.TestServers;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Lodgify.Cinema.AcceptanceTest.StepDefinitions
{
    [Binding]
    public sealed class ImdbStatusStepDefinitions
    {
        private HttpResponseMessage _apiCallResult;

        [When("i call the HealthCheck Api")]
        public void i_call_the_HealthCheck_Api()
        {
            var message = MoviesApiTestServer.Client.Get("Status",null, token: MoviesApiTestServer.ReadOnlyToken);
            _apiCallResult = MoviesApiTestServer.Client.SendAsync(message).Result;
        }
            
        [Then("i get the health about imdb api")]
        public void i_get_the_health_about_imdb_api()
        {
            _apiCallResult.EnsureSuccessStatusCode();
        }
    }
}