using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseData.Interfaces
{
    public interface IRepoBase
    {
        Task Create<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;
        Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> ReadSingle<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> ReadSingle<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeExpressions) where TEntity : class;
        Task<List<TEntity>> ReadMultiple<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TEntity : class;
        Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;
        Task Delete<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;
        Task Delete<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TEntity : class;
    }
}
