using FluentAssertions;
using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.DomainService.Imdb;
using Moq;
using System.Threading;
using Xunit;

namespace Lodgify.Cinema.UnitTest.DomainService
{
    public class ImdbStatusServiceTest
    {
        private readonly Mock<IImdbRepository> _imdbRepository;

        public ImdbStatusServiceTest()
        {
            _imdbRepository = new Mock<IImdbRepository>();
        }

        [Trait("DomainService", "ImdbStatusService")]
        [Fact]
        public async void TranslateStringToIntImdbIdSuccess()
        {
            //Arranje
            bool mockResponse = true;
            _imdbRepository.Setup(s => s.HealtCheckStatusAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);
            IImdbStatusService _imdbStatusService = new ImdbStatusService(_imdbRepository.Object);

            //Act
            var response = await _imdbStatusService.IsUpAsync(CancellationToken.None);

            //Assert
            _imdbRepository.Verify(s => s.HealtCheckStatusAsync(It.IsAny<CancellationToken>()), Times.Once);
            response.IsUp.Should().BeTrue();
        }
    }
}