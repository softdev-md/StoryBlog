using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Application.Caching;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;
using WebApp.Api.Persistence.Extensions;

namespace WebApp.Api.Persistence.Repositories
{
    /// <summary>
    /// Post service
    /// </summary>
    public partial class PostRepository : BaseRepository<Post>, IPostRepository
    {
        #region Ctor

        public PostRepository(ApiDbContext dbContext, ICacheManager staticCacheManager) : base(dbContext, staticCacheManager)
        {
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="postCategoryIds"></param>
        /// <param name="postStatusId"></param>
        /// <param name="keywords"></param>
        /// <param name="tags"></param>
        /// <param name="publishedOnDate"></param>
        /// <param name="sortedRandom"></param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Posts</returns>
        public async Task<IPagedList<Post>> GetAllPostsAsync(int projectId = 0, IList<int> postCategoryIds = null, 
            int postStatusId = 0, string keywords = null, string tags = null, 
            DateTime? publishedOnDate = null, bool sortedRandom = false,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return await GetAllPagedAsync(query =>
            {
                if (projectId > 0)
                    query = query.Where(p => p.ProjectId == projectId);

                if (postCategoryIds is not null)
                {
                    if (postCategoryIds.Contains(0))
                        postCategoryIds.Remove(0);

                    if (postCategoryIds.Any())
                        query = query.Where(p => postCategoryIds.Contains(p.PostCategoryId));
                }

                if (postStatusId > 0)
                    query = query.Where(p => p.PostStatusId == postStatusId);

                if (!string.IsNullOrEmpty(keywords))
                    query = query.Where(p => p.Body.Contains(keywords));

                if (!string.IsNullOrEmpty(tags))
                    query = query.Where(p => p.Tags.Contains(tags));

                if (publishedOnDate != null)
                    query = query.Where(p => p.PublishedOn.Value.Date == publishedOnDate.Value.Date);

                query = sortedRandom
                    ? query.OrderBy(p => Guid.NewGuid())
                    : query.OrderByDescending(p => p.DisplayOrder);

                return query;
            }, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets post
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="postCategoryId"></param>
        /// <param name="postStatusId"></param>
        /// <param name="sortedByRating"></param>
        /// <param name="keywords"></param>
        /// <param name="tags"></param>
        /// <returns>Post</returns>
        public async Task<Post> GetPostAsync(int projectId = 0, int postCategoryId = 0, int postStatusId = 0, 
            bool sortedByRating = false, string keywords = null, string tags = null)
        {
            var query = Table;

            if (projectId > 0)
                query = query.Where(p => p.ProjectId == projectId);

            if (postCategoryId > 0)
                query = query.Where(p => p.PostCategoryId == postCategoryId);

            if (postStatusId > 0)
                query = query.Where(p => p.PostStatusId == postStatusId);

            if (!string.IsNullOrEmpty(keywords))
                query = query.Where(p => p.Body.Contains(keywords));

            if (!string.IsNullOrEmpty(tags))
                query = query.Where(p => p.Tags.Contains(tags));

            query = sortedByRating
                ? query.OrderByDescending(p => (p.CommentsCount + p.DislikesCount) / 100)
                : query.OrderBy(p => Guid.NewGuid());

            var post = await query.FirstOrDefaultAsync(CancellationToken.None);
            return post;
        }
        
        #endregion
    }
}