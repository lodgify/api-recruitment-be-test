using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApiApplication.Tests.Infrastructure
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection : ICollectionFixture<TestApplicationFactory<Program>>
    {
    }
}
