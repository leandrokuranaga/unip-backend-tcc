using Microsoft.EntityFrameworkCore;
using Motorcycle.Domain.SeedWork;
using System.Linq.Expressions;

namespace Motorcycle.Infra.Data.Repository
{
    public abstract class BaseRepository<TContext, TEntity> : IBaseRepository<TContext, TEntity> where TContext : DbContext where TEntity : class
    {
        private readonly TContext _genericContext;
        public BaseRepository(TContext context)
        {
            _genericContext = context;
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            await _genericContext.Set<TEntity>().AddAsync(entity);
            await _genericContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await _genericContext.Set<TEntity>().AddRangeAsync(entity);
            await _genericContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _genericContext.Set<TEntity>().Update(entity);
            await _genericContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll() => _genericContext.Set<TEntity>().AsQueryable();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression) => await GetAll().Where(expression).ToListAsync();

        public async Task<TEntity> GetByIdAsync(int id, bool noTracking) => await _genericContext.Set<TEntity>().FindAsync(id);

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression) => GetAll().Where(expression);

        public virtual IQueryable<TEntity> Include<TProperty>(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> path) => query.Include(path);

        public async Task DeleteAsync(TEntity entity)
        {
            _genericContext.Set<TEntity>().Remove(entity);
            await _genericContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await GetAll().ToListAsync();

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression) => await _genericContext.Set<TEntity>().AsNoTracking().AnyAsync(expression);

        public async Task<TEntity> GetOneNoTracking(Expression<Func<TEntity, bool>> expression) => await _genericContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);

        public async Task<TEntity> GetOneTracking(Expression<Func<TEntity, bool>> expression) => await _genericContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);
        public async Task<IEnumerable<TEntity>> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression) => await GetAll().AsNoTracking().Where(expression).ToListAsync();

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _genericContext.Set<TEntity>().AddRangeAsync(entities);
            await _genericContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _genericContext.Set<TEntity>().UpdateRange(entities);
            await _genericContext.SaveChangesAsync();
        }

        public async Task UpdateRangeNoTrackingAsync(IEnumerable<TEntity> entities)
        {
            _genericContext.AttachRange(entities);
            _genericContext.Set<TEntity>().UpdateRange(entities);
            await _genericContext.SaveChangesAsync();
        }
    }
}
