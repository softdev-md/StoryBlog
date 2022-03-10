using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Web.Front.ApiDefinitions;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Models.Common;

namespace WebApp.Web.Front.Services
{
    public interface IPostService
    {
        public Task<DataSourceResult<Post>> GetAllPostsAsync(int projectId, int categoryId = 0, string keyword = null, 
            int pageIndex = 0, int pageSize = int.MaxValue);

        public Task<Post> GetPostById(int id);
    }

    public class PostService : IPostService
    {
        private readonly IPostApi _postApi;

        public PostService(IPostApi postApi)
        {
            _postApi = postApi;
        }

        public async Task<DataSourceResult<Post>> GetAllPostsAsync(int projectId, int categoryId = 0, string keyword = null, 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = await _postApi.GetAllPostsAsync(projectId, categoryId, keyword, pageIndex, pageSize);
            
            return result;
        }

        public async Task<Post> GetPostById(int id)
        {
            var result = await _postApi.GetPostById(id);
            
            return result;
        }
    }
}
