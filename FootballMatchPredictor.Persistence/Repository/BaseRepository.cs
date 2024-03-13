using FootballMatchPredictor.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Persistence.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            ValidateEntityOnNull(entity);

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            ValidateEntityOnNull(entity);

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            ValidateEntityOnNull(entity);

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        private void ValidateEntityOnNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity is null");
            }
        }
    }
}
