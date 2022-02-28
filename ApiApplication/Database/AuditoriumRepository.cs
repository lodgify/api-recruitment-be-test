using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly CinemaContext _context;
        public AuditoriumRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<AuditoriumEntity> Add(AuditoriumEntity auditoriumEntity)
        {
            await _context.Auditoriums.AddAsync(auditoriumEntity);
            await _context.SaveChangesAsync();
            return auditoriumEntity;
        }

        public async Task<AuditoriumEntity> Delete(int id)
        {
            var auditoriumEntity = await _context.Auditoriums.FindAsync(id);
            _context.Auditoriums.Remove(auditoriumEntity);
            await _context.SaveChangesAsync();
            return auditoriumEntity;
        }

        public async Task<IEnumerable<AuditoriumEntity>> GetCollection()
        {
            return await _context.Auditoriums.ToListAsync();
        }

        public async Task<AuditoriumEntity> Get(int id)
        {
            var auditoriumEntity = await _context.Auditoriums.FindAsync(id);
            return auditoriumEntity;
        }

        public async Task<AuditoriumEntity> Update(AuditoriumEntity auditoriumEntity)
        {
            var entity = await _context.Auditoriums.FindAsync(auditoriumEntity.Id);
            if (entity == null)
            {
                return null;
            }
            _context.Entry(entity).CurrentValues.SetValues(auditoriumEntity);
            await _context.SaveChangesAsync();
            return auditoriumEntity;

        }

        //public async Task<AuditoriumEntity> AddShowtime(int auditoriumId, ShowtimeEntity showtimeEntity)
        //{
        //    var auditoriumEntity = await Get(auditoriumId);
        //    if (auditoriumEntity.Showtimes == null)
        //    {
        //        auditoriumEntity.Showtimes = new List<ShowtimeEntity>();
        //    }
        //    auditoriumEntity.Showtimes.Add(showtimeEntity);
        //    await Update(auditoriumEntity);
        //    return auditoriumEntity;
        //}
    }
}
