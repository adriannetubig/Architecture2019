using BaseData.Entities;
using BaseData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public async Task<EntityPagedList<TEntity>> ReadMultiple<TEntity>(Expression<Func<TEntity, bool>> predicate, bool ascending, int itemsPerPage, int pageNo, string sortBy, CancellationToken cancellationToken)
            where TEntity : class
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate);

            return await PagedResult(entities, ascending, itemsPerPage, pageNo, sortBy, cancellationToken);
        }
        public async Task<EntityPagedList<TEntity>> ReadMultiple<TEntity>(Expression<Func<TEntity, bool>> predicate, bool ascending, int itemsPerPage, int pageNo, string sortBy, CancellationToken cancellationToken,
            params Expression<Func<TEntity, object>>[] includeExpressions) where TEntity : class
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate);

            entities = includeExpressions.Aggregate(entities, (entity, includeEntity) => entity.Include(includeEntity));

            return await PagedResult(entities, ascending, itemsPerPage, pageNo, sortBy, cancellationToken);
        }

        public async Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update<TEntity>(List<TEntity> entities, CancellationToken cancellationToken) where TEntity : class
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
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

        private async Task<EntityPagedList<TEntity>> PagedResult<TEntity>(IQueryable<TEntity> entities, bool ascending, int itemsPerPage, int pageNo, string sortBy, CancellationToken cancellationToken)
            where TEntity : class
        {
            var resultCount = entities.Count();
            int excludedRows = (pageNo - 1) * itemsPerPage;

            entities = OrderBy(entities, sortBy, ascending);

            EntityPagedList<TEntity> resultEPagedList = new EntityPagedList<TEntity>
            {
                PageNo = pageNo,
                Items = await entities.Skip(excludedRows).Take(itemsPerPage).ToListAsync(cancellationToken),
                ItemsPerPage = itemsPerPage,
                NumberOfItems = resultCount
            };

            return resultEPagedList;
        }

        private IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> entities, string sortColumn) where TEntity : class
        {
            return OrderBy(entities, sortColumn, true);
        }

        private IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> entities, string sortColumn, bool sortAscending) where TEntity : class
        {
            var propertyInfo = GetPropertyInfo(typeof(TEntity), sortColumn);
            var expression = GetOrderExpression(typeof(TEntity), propertyInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == (sortAscending ? "OrderBy" : "OrderByDescending") && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { entities, expression });
        }

        private PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }

        private LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpression = Expression.Parameter(objType);
            var propertyAccess = Expression.PropertyOrField(paramExpression, pi.Name);
            var expression = Expression.Lambda(propertyAccess, paramExpression);
            return expression;
        }
    }
}
