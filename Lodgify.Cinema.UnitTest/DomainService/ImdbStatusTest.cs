using FluentAssertions;
using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.DomainService.Imdb;
using System;
using Xunit;

namespace Lodgify.Cinema.UnitTest.DomainService
{
    public class ImdbStatusTest
    {
        [Trait("DomainService", "ImdbStatus")]
        [Fact]
        public void ImdbStatusConstrutorSuccess()
        {
            //Arranje/Act
            var now = DateTime.Now;
            IImdbStatus imdbIdTranslatorService = new ImdbStatus(true, now);

            //Assert
            imdbIdTranslatorService.LastExceptionThrow.HasValue.Should().BeFalse();
            imdbIdTranslatorService.IsUp.Should().BeTrue();
            imdbIdTranslatorService.LastStatusCheck.Should().Be(now);
            imdbIdTranslatorService.FirstCheckDone.Should().BeFalse();
            imdbIdTranslatorService.LastException.Should().BeNullOrEmpty();
        }

        [Trait("DomainService", "ImdbStatus")]
        [Fact]
        public void ImdbStatusSetCheckBindSuccess()
        {
            //Arranje
            IImdbStatus imdbIdTranslatorService = new ImdbStatus(false, null);

            //Act
            bool status = true;
            DateTime now = DateTime.Now;
            imdbIdTranslatorService.SetCheck(status, now);


            //Assert
            imdbIdTranslatorService.IsUp.Should().BeTrue();
            imdbIdTranslatorService.LastStatusCheck.Should().Be(now);
            imdbIdTranslatorService.FirstCheckDone.Should().BeTrue();
            imdbIdTranslatorService.LastExceptionThrow.HasValue.Should().BeFalse();
            imdbIdTranslatorService.LastException.Should().BeNullOrEmpty();
        }

        [Trait("DomainService", "ImdbStatus")]
        [Fact]
        public void ImdbStatusSetExceptionBindSuccess()
        {
            //Arranje
            IImdbStatus imdbIdTranslatorService = new ImdbStatus(false, null);

            //Act
            string errorMessage = "error";
            DateTime now = DateTime.Now;
            imdbIdTranslatorService.SetException(new Exception(errorMessage), now);


            //Assert
            imdbIdTranslatorService.IsUp.Should().BeFalse();
            imdbIdTranslatorService.LastStatusCheck.Should().BeNull();
            imdbIdTranslatorService.FirstCheckDone.Should().BeFalse();
            imdbIdTranslatorService.LastExceptionThrow.Should().Be(now);
            imdbIdTranslatorService.LastException.Should().Be(errorMessage);
        }
    }
}