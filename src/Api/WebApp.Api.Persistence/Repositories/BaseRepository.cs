using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Application.Caching;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Common;
using WebApp.Api.Persistence.Extensions;

namespace WebApp.Api.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        #region Fields

        protected readonly ApiDbContext _dbContext;
        private readonly ICacheManager _staticCacheManager;

        private DbSet<TEntity> _entities;

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected DbSet<TEntity> Entities => _entities ?? (_entities = _dbContext.Set<TEntity>());

        #endregion

        #region Utilities

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="getAllAsync">Function to select entries</param>
        /// <param name="cacheKey">Cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>Entity entries</returns>
        protected Task<IList<TEntity>> GetEntities(Func<Task<IList<TEntity>>> getAllAsync, string cacheKey = null)
        {
            if (cacheKey == null)
                return getAllAsync();

            //caching
            return _staticCacheManager.GetAsync(cacheKey, getAllAsync);
        }
        
        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected async Task<string> GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_dbContext is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            await _dbContext.SaveChangesAsync();
            return exception.ToString();
        }

        #endregion

        #region Ctor

        public BaseRepository(ApiDbContext dbContext, ICacheManager staticCacheManager)
        {
            _dbContext = dbContext;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Get entity entries by identifiers
        /// </summary>
        /// <param name="ids">Entity entry identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();
            
            //get entries
            var entries = await Table.Where(entry => ids.Contains(entry.Id)).ToListAsync();
            
            return entries;
        }

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="cacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, 
            string cacheKey = null)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = func != null ? func(Table) : Table;
                return await query.AsNoTracking().ToListAsync();
            }

            return await GetEntities(getAllAsync, cacheKey);
        }
        
        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="cacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public async Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
            string cacheKey = null)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = await func(Table);
                return await query.AsNoTracking().ToListAsync();
            }

            return await GetEntities(getAllAsync, cacheKey);
        }
        
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
        public Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = func != null ? func(Table) : Table;

            return query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }
        
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
        public async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = func != null ? await func(Table) : Table;

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Add(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.AddRange(entities);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        #endregion
    }
}
