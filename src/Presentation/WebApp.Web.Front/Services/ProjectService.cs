using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Web.Front.ApiDefinitions;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Models.Common;

namespace WebApp.Web.Front.Services
{
    public interface IProjectService
    {
        public Task<List<Project>> GetAllPostCategoriesAsync(int projectId = 0);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectApi _projectApi;

        public ProjectService(IProjectApi projectApi)
        {
            _projectApi = projectApi;
        }
        
        public async Task<List<Project>> GetAllPostCategoriesAsync(int projectId = 0)
        {
            var result = await _projectApi.GetAllProjectsAsync();
            
            return result;
        }
    }
}
