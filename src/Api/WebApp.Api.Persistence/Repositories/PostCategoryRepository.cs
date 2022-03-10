using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Api.Application.Caching;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Persistence.Repositories
{
    /// <summary>
    /// PostCategory service
    /// </summary>
    public partial class PostCategoryRepository : BaseRepository<PostCategory>, IPostCategoryRepository
    {
        #region Consts

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : project id
        /// </remarks>
        private const string PostCategoriesAllCacheKey = "WebApp.Api.PostCategory.All-{0}";

        #endregion
        
        #region Ctor

        public PostCategoryRepository(ApiDbContext dbContext, ICacheManager staticCacheManager) : base(dbContext, staticCacheManager)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all postCategories
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <returns>PostCategories</returns>
        public virtual async Task<IList<PostCategory>> GetAllPostCategoriesAsync(int projectId = 0)
        {
            var key = string.Format(PostCategoriesAllCacheKey, projectId);
            var postCategories = await GetAllAsync(query =>
            {
                if (projectId > 0)
                    query = query.Where(p => p.ProjectId == projectId);

                return query;
            }, key);
            return postCategories;
        }
        
        #endregion
    }
}