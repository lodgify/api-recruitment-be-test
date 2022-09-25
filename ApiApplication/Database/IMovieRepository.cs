using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieEntity>> GetAsync(Expression<Func<MovieEntity, bool>> filter);
    }
}
