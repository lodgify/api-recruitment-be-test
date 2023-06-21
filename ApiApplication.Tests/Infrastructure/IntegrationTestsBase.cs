using ApiApplication.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace ApiApplication.Tests.Infrastructure
{
    public class IntegrationTestsBase : IDisposable
    {
        protected CinemaContext Context { get; set; }

        protected IServiceScope ServiceScope { get; }

        protected HttpClient Client { get; private set; }

        protected WebApplicationFactory<Program> ApplicationFactory { get; set; }

        protected virtual void Initialize() { }

        public IntegrationTestsBase([NotNull] TestApplicationFactory<Program> applicationFactory)
        {
            ApplicationFactory = applicationFactory;
            ServiceScope = ApplicationFactory.Services.CreateScope();

            Initialize();
            Client = ApplicationFactory.CreateClient();
            Context = ServiceScope.ServiceProvider.GetService<CinemaContext>();
        }

        public void Dispose()
        {
            ServiceScope.Dispose();
            Client.Dispose();
        }
    }
}
