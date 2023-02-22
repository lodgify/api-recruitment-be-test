using CinemaApplication.DAL.Abstractions;
using CinemaApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Repositories
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly CinemaContext _dbContext;

        public AuditoriumRepository(CinemaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuditoriumEntity> GetAsync(int id)
            => await _dbContext.Auditoriums
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
    }
}
