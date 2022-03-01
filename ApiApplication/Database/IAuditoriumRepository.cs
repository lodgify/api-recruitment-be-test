using ApiApplication.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public interface IAuditoriumRepository
    {
        Task<AuditoriumEntity> Add(AuditoriumEntity auditoriumEntity);

        Task<AuditoriumEntity> Delete(int id);

        Task<IEnumerable<AuditoriumEntity>> GetCollection();

        Task<AuditoriumEntity> Get(int id);

        Task<AuditoriumEntity> Update(AuditoriumEntity auditoriumEntity);
        //Task<AuditoriumEntity> AddShowtime(int auditoriumId, ShowtimeEntity showtimeEntity);
    }
}
