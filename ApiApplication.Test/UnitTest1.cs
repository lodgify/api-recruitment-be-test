using System;
using Xunit;
using ApiApplication.Services;

namespace ApiApplication.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestImdbService()
        {
            ImdbStatusService service = new ImdbStatusService();
            Assert.False(service.Up);
        }
    }
}
