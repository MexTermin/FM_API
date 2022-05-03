global using FM_API.Entities;
global using Microsoft.EntityFrameworkCore;

using FM_API.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Linq.Expressions;


#nullable disable

namespace FM_API.Persistance.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected DbContext _DataContext;

        public GenericRepository(DbContext _context)
        {
            _DataContext = _context;
            _dbSet = _DataContext.Set<T>();
        }
        public async Task<T> Create(T entity)
        {
            _dbSet.Add(entity);
            await _DataContext.SaveChangesAsync();

            return entity;
        }

        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _DataContext.Entry(entity).State = EntityState.Modified;
            await _DataContext.SaveChangesAsync();
        }

        public async Task Delete(long idEntity)
        {
            var entity = _dbSet.Find(idEntity);
            _dbSet.Remove(entity);
            await _DataContext.SaveChangesAsync();
        }

        public async Task DeleteAll(Expression<Func<T, bool>> where)
        {
            var entities = await _dbSet.Where(where).ToListAsync();
            _dbSet.RemoveRange(entities);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<T> Find(long idEntity)
        {
            return await _dbSet.FindAsync(idEntity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }       
        
        public async Task<T> GetWithDelete(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.IgnoreQueryFilters().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbSet.CountAsync();
            return await _dbSet.CountAsync(predicate);
        }

        public virtual async Task<(IEnumerable<T>, int)> GetPage<TOrder>(PageInfo filter, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, SortOrder sortOrder)
        {
            if (sortOrder.Equals(SortOrder.Ascending))
            {
                return (
                    await _DataContext.Set<T>()
                    .Where(where)
                    .OrderBy(order)
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .ToArrayAsync(),
                    await _DataContext.Set<T>()
                    .AsNoTracking()
                    .Where(where)
                    .CountAsync());
            }
            else
            {
                return (
                    await _DataContext.Set<T>()
                    .Where(where)
                    .OrderByDescending(order)
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .ToArrayAsync(),
                    await _DataContext.Set<T>()
                    .AsNoTracking()
                    .Where(where)
                    .CountAsync());
            }
        }
    }
}
