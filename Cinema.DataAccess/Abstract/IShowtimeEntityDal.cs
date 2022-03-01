using Cinema.Core.DataAccess;
using Cinema.Entities.Concrete;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Abstract
{
    public interface IShowtimeEntityDal : IEntityRepository<ShowtimeEntity>
    {
    }
}
