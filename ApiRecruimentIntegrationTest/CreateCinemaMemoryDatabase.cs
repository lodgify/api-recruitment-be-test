using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiRecruimentIntegrationTest
{
    public class CreateCinemaMemoryDatabase
    {
        private CinemaContext _context;
        public CinemaContext CreateDatabase(string name= "Test_Database")
        {
           
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseInMemoryDatabase(databaseName: name).Options;
         
            _context = new CinemaContext(options);
            
            return _context;

        }

    }
}
