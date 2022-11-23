using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerSpot.IntegrationTests.Repositories.Base
{
    public abstract class RepositoryTestFixture
    {
        protected CinemaContext _dbContext;

        public RepositoryTestFixture()
        {
            var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<CinemaContext>();

            builder.UseInMemoryDatabase("InMemoryDb.ApiApplicationDb")
                   .UseInternalServiceProvider(serviceProvider);

            _dbContext = new CinemaContext(builder.Options);
        }

        protected IShowtimesRepository GetShowtimeRepository()
        {
            return new ShowtimesRepository(_dbContext);
        }
    }
}
