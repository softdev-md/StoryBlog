using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Web.Front.ApiDefinitions;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Models.Common;

namespace WebApp.Web.Front.Services
{
    public interface IPostCategoryService
    {
        public Task<List<PostCategory>> GetAllPostCategoriesAsync(int projectId = 0);
    }

    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryApi _postCategoryApi;

        public PostCategoryService(IPostCategoryApi postCategoryApi)
        {
            _postCategoryApi = postCategoryApi;
        }
        
        public async Task<List<PostCategory>> GetAllPostCategoriesAsync(int projectId = 0)
        {
            var result = await _postCategoryApi.GetAllPostCategoriesAsync(projectId);
            
            return result;
        }
    }
}
