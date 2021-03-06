using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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
        private readonly IHttpClientFactory _clientFactory;

        public PostService(IPostApi postApi, 
            IHttpClientFactory clientFactory)
        {
            _postApi = postApi;
            _clientFactory = clientFactory;
        }

        public async Task<DataSourceResult<Post>> GetAllPostsAsync(int projectId, int categoryId = 0, string keyword = null, 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var result = await _postApi.GetAllPostsAsync(projectId, categoryId, keyword, pageIndex, pageSize);

                //var client = _clientFactory.CreateClient("storyBlogApi");
                //var result = await client.GetFromJsonAsync<DataSourceResult<Post>>("/api/post");

                return result;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            
            return null;
        }

        public async Task<Post> GetPostById(int id)
        {
            var result = await _postApi.GetPostById(id);
            
            return result;
        }
    }
}
