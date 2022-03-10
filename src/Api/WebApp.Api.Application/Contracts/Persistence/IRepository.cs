using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Common;

namespace WebApp.Api.Application.Contracts.Persistence
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }
    
        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Get entity entries by identifiers
        /// </summary>
        /// <param name="ids">Entity entry identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetByIdsAsync(IList<int> ids);

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="cacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            string cacheKey = null);

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="cacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
            string cacheKey = null);

        /// <summary>
        /// Get paged list of all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">Whether to get only the total number of entries without actually loading data</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the paged list of entity entries
        /// </returns>
        Task<IPagedList<TEntity>> GetAllPagedAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        /// <summary>
        /// Get paged list of all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">Whether to get only the total number of entries without actually loading data</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the paged list of entity entries
        /// </returns>
        Task<IPagedList<TEntity>> GetAllPagedAsync(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
