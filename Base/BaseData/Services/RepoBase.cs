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
        public async Task<EPagedList<TEntity>> ReadMultiple<TEntity>(Expression<Func<TEntity, bool>> predicate, EPageFilter ePageFilter, CancellationToken cancellationToken) where TEntity : class
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate);

            return await PagedResult(entities, ePageFilter, cancellationToken);
        }
        public async Task<EPagedList<TEntity>> ReadMultiple<TEntity>(Expression<Func<TEntity, bool>> predicate, EPageFilter ePageFilter, CancellationToken cancellationToken,
            params Expression<Func<TEntity, object>>[] includeExpressions) where TEntity : class
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate);

            entities = includeExpressions.Aggregate(entities, (entity, includeEntity) => entity.Include(includeEntity));

            return await PagedResult(entities, ePageFilter, cancellationToken);
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

        private async Task<EPagedList<TEntity>> PagedResult<TEntity>(IQueryable<TEntity> entities, EPageFilter ePageFilter, CancellationToken cancellationToken) where TEntity : class
        {
            var itemsPerPage = 20;
            var pageNo = 1;

            if (ePageFilter.ItemsPerPage > 0)
                itemsPerPage = ePageFilter.ItemsPerPage;

            var resultCount = entities.Count();

            if (resultCount > itemsPerPage && ePageFilter.ItemsPerPage > 0)
                pageNo = ePageFilter.PageNo;

            resultCount = entities.Count();

            int excludedRows = (pageNo - 1) * itemsPerPage;

            entities = OrderBy(entities, ePageFilter.Ascending, ePageFilter.SortBy);

            EPagedList<TEntity> resultEPagedList = new EPagedList<TEntity>
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
            var propInfo = GetPropertyInfo(typeof(TEntity), sortColumn);
            var expr = GetOrderExpression(typeof(TEntity), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(TEntity), propInfo.PropertyType);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { entities, expr });
        }

        private IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> entities, bool sortAscending, string sortColumn) where TEntity : class
        {
            var propInfo = GetPropertyInfo(typeof(TEntity), sortColumn);
            var expr = GetOrderExpression(typeof(TEntity), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == (sortAscending ? "OrderBy" : "OrderByDescending") && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(TEntity), propInfo.PropertyType);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { entities, expr });
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
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }
    }
}
