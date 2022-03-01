using Cinema.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
       where TEntity : class, IEntity, new()
       where TContext : DbContext
    {
        private readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            var process = await _context.Set<TEntity>().AsNoTracking().AnyAsync(filterExpression);
            return process;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            var process = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filterExpression);
            return process;
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            var process = (filterExpression == null ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>().Where(filterExpression).AsNoTracking());

            return process;
        }

        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            var process = await (filterExpression == null
                ? _context.Set<TEntity>().AsNoTracking().ToListAsync()
                : _context.Set<TEntity>().Where(filterExpression).AsNoTracking().ToListAsync());

            return process;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addEntity = await _context.AddAsync(entity);
            addEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var updateEntity = _context.Update(entity);
            updateEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var deleteEntity = _context.Remove(entity);
            deleteEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();

            IQueryable<TEntity> query = null;
            foreach (var includeExpression in includeExpressions)
            {
                query = dbSet.Include(includeExpression);
            }

            return query ?? dbSet;
        }

    }
}
