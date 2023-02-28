using FluentAssertions;
using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.DomainService.Imdb;
using System;
using Xunit;

namespace Lodgify.Cinema.UnitTest.DomainService
{
    public class ImdbIdTranslatorServiceTest
    {
        [Trait("DomainService", "ImdbIdTranslatorService")]
        [Theory]
        [InlineData(1234)]
        [InlineData(0)]
        [InlineData(99999)]
        public void TranslateStringToIntImdbIdSuccess(int numberId)
        {
            //Arranje
            IImdbIdTranslatorService imdbIdTranslatorService = new ImdbIdTranslatorService();
            string stringId = $"{imdbIdTranslatorService.ImdbPrefixId}{numberId}";

            //Act
            int response = imdbIdTranslatorService.Get(stringId);

            //Assert
            response.Should().Be(numberId);
        }

        [Trait("DomainService", "ImdbIdTranslatorService")]
        [Theory]
        [InlineData(1234)]
        [InlineData(0)]
        [InlineData(99999)]
        public void TranslateIntToStringImdbIdSuccess(int id)
        {
            //Arranje
            IImdbIdTranslatorService imdbIdTranslatorService = new ImdbIdTranslatorService();

            //Act
            string response = imdbIdTranslatorService.Get(id);

            //Assert
            response.Should().StartWith(imdbIdTranslatorService.ImdbPrefixId);
        }
    }
}
