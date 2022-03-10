using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Contracts.Persistence
{
    /// <summary>
    /// Project repository interface
    /// </summary>
    public partial interface IProjectRepository : IRepository<Project>
    {
        #region Project accounts

        /// <summary>
        /// Gets a project accounts by project identifier
        /// </summary>
        /// <param name="projectId">The project identifier</param>
        /// <returns>Project pictures</returns>
        Task<IList<ProjectAccount>> GetProjectAccountsByProjectIdAsync(int projectId);

        /// <summary>
        /// Gets a project pictures by project identifier
        /// </summary>
        /// <param name="projectId">The project identifier</param>
        /// <param name="accountTypeId"></param>
        /// <returns>Project pictures</returns>
        //Task<ProjectAccount> GetProjectAccountByProjectIdAsync(int projectId, int accountTypeId);

        #endregion
    }
}