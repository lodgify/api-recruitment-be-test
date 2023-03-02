using FluentAssertions;
using Lodgify.Cinema.Domain.Contract.Log;
using Lodgify.Cinema.DomainService.Log;
using System;
using Xunit;

namespace Lodgify.Cinema.UnitTest
{
    public class LogServiceTest
    {
        [Trait("DomainService", "LogService")]
        [Fact]
        public void LogSendSuccess()
        {
            //Arranje
            ILodgifyLogService log = new LodgifyLogService();

            //Act
            Action action = () => log.Log("Test");

            //Assert
            action.Should().NotThrow();
        }
    }
}
