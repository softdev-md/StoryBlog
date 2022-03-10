using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Contracts.Persistence
{
    /// <summary>
    /// PostCategory repository interface
    /// </summary>
    public partial interface IPostCategoryRepository : IRepository<PostCategory>
    {
        /// <summary>
        /// Gets all postCategories
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <returns>PostCategories</returns>
        Task<IList<PostCategory>> GetAllPostCategoriesAsync(int projectId = 0);
    }
}