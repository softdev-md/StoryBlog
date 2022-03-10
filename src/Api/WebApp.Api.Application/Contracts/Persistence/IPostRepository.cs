using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Contracts.Persistence
{
    /// <summary>
    /// Post repository interface
    /// </summary>
    public partial interface IPostRepository : IRepository<Post>
    {
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
        Task<IPagedList<Post>> GetAllPostsAsync(int projectId = 0, IList<int> postCategoryIds = null, 
            int postStatusId = 0, string keywords = null, string tags = null, 
            DateTime? publishedOnDate = null, bool sortedRandom = false,
            int pageIndex = 0, int pageSize = int.MaxValue);

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
        Task<Post> GetPostAsync(int projectId = 0, int postCategoryId = 0, int postStatusId = 0,
            bool sortedByRating = false, string keywords = null, string tags = null);
    }
}