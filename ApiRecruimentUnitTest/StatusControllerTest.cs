using ApiApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiRecruimentUnitTest
{
    public class StatusControllerTest
    {

        private StatusController _controller;

        public StatusControllerTest()
        {
            _controller = new StatusController();
        }


        [Fact]
        public void Get_ShouldReturnStatusCode200()
        {
            // Arrange            
            //Act
            IActionResult actionResult =  _controller.Get();
            var contentResult = actionResult as OkObjectResult;
            int status = contentResult.StatusCode.Value;
            // Assert
            Assert.NotNull(contentResult);
            Assert.Equal(200, status);
        }
    }
}