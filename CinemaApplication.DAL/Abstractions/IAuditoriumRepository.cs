using CinemaApplication.DAL.Models;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Abstractions
{
    public interface IAuditoriumRepository
    {
        Task<AuditoriumEntity> GetAsync(int id);
    }
}
