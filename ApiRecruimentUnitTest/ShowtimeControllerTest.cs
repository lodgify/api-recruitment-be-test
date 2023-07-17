using ApiApplication.Controllers;
using ApiApplication.Database;
using ApiApplication.DTO;
using ApiApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApiRecruimentUnitTest
{
    public class ShowtimeControllerTest

    {
        private Mock<IShowtimeService> _mockShowtimeService;
        private ShowtimeController _controller;

        public ShowtimeControllerTest()
        {
            _mockShowtimeService = new Mock<IShowtimeService>();
        
            _mockShowtimeService.Setup(x => x.Add(It.IsAny<ShowtimeCommand>())).ReturnsAsync(TestData.TestData.CreateShowtimeWithMovie());
            _mockShowtimeService.Setup(x => x.Update(It.IsAny<ShowtimeCommand>())).ReturnsAsync(TestData.TestData.CreateShowtimeWithMovie());

            _controller = new ShowtimeController(_mockShowtimeService.Object);

        }


        [Fact]
        public async Task Post_ShouldReturnStatusCode200()
        {
            // Arrange            
            //Act
            IActionResult actionResult = await _controller.Post(TestData.TestData.CreateCommandShowTime(1));
            var contentResult = actionResult as OkObjectResult;
            int status = contentResult.StatusCode.Value;
            // Assert
            Assert.NotNull(contentResult);       
            Assert.Equal(200, status);
        }


        [Fact]
        public async Task Put_ShouldReturnStatusCode200()
        {
            // Arrange            
            //Act
            IActionResult actionResult = await _controller.Put(TestData.TestData.CreateCommandShowTime(1));
            var contentResult = actionResult as OkObjectResult;
            int status = contentResult.StatusCode.Value;
            // Assert
            Assert.NotNull(contentResult);
            Assert.Equal(200, status);
        }

        [Fact]
        public void  Patch_ShouldThrowException()
        {
            // Arrange            
            //Act           
            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Patch());
           


        }

    }
}