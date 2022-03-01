using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> filterExpression);
        Task<T> GetAsync(Expression<Func<T, bool>> filterExpression);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> filterExpression = null);
        Task<IList<T>> GetListAsync(Expression<Func<T, bool>> filterExpression = null);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includeExpressions);
       
    }
}
