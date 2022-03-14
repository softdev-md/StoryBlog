using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Models.Common;

namespace WebApp.Web.Front.ApiDefinitions
{
    /// <summary>
    /// Post Api Definition
    /// </summary>
    public interface IPostApi
    {
        [Get("/api/post")]
        [Headers("Authorization: Bearer")]
        Task<DataSourceResult<Post>> GetAllPostsAsync(int projectId, int categoryId, string keyword, int page, int pageSize);

        [Get("/api/post/{id}")]
        Task<Post> GetPostById(int id);
    }
}