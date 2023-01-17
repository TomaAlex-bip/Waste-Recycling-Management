using Microsoft.EntityFrameworkCore;
using WasteRecyclingManagementApi.Core.Repositories;

namespace WasteRecyclingManagementApi.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly RecyclingDbContext _dbContext;

        public Repository(RecyclingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var entry = _dbContext.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
            return entity;
        }
    }
}
