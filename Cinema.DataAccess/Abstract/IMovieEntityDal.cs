using Cinema.Core.DataAccess;
using Cinema.Entities.Concrete;

namespace Cinema.DataAccess.Abstract
{
    public interface IMovieEntityDal : IEntityRepository<MovieEntity>
    {
    }
}
