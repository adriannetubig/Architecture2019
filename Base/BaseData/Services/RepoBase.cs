using BaseData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseData.Services
{
    public class RepoBase: IRepoBase
    {
        private readonly DbContext _dbContext;
        public RepoBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .AnyAsync(predicate, cancellationToken);
        }
        public async Task<TEntity> ReadSingle<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(expression, cancellationToken);
        }
        public async Task<TEntity> ReadSingle<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeExpressions) where TEntity : class
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .AsQueryable();

            return await includeExpressions.Aggregate(entities, (entity, includeEntity) => entity.Include(includeEntity)).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<TEntity>> ReadMultiple<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(expression)
                .ToListAsync(cancellationToken);
        }

        public async Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task Delete<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TEntity : class
        {
            var entity = await ReadSingle(expression, cancellationToken);
            await Delete(entity, cancellationToken);
        }
    }
}
