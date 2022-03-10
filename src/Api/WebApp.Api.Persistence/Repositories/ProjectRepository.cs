using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Application.Caching;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Utils.Paging;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Persistence.Repositories
{
    /// <summary>
    /// Project service
    /// </summary>
    public partial class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        #region Ctor

        public ProjectRepository(ApiDbContext dbContext, ICacheManager staticCacheManager) : base(dbContext, staticCacheManager)
        {
        }

        #endregion
        
        #region Project accounts

        /// <summary>
        /// Gets a project accounts by project identifier
        /// </summary>
        /// <param name="projectId">The project identifier</param>
        /// <returns>Project pictures</returns>
        public virtual async Task<IList<ProjectAccount>> GetProjectAccountsByProjectIdAsync(int projectId)
        {
            var query = from pp in _dbContext.ProjectAccounts
                where pp.ProjectId == projectId
                select pp;
            var projectAccounts = await query.ToListAsync();
            return projectAccounts;
        }

        /// <summary>
        /// Gets a project pictures by project identifier
        /// </summary>
        /// <param name="projectId">The project identifier</param>
        /// <param name="accountTypeId"></param>
        /// <returns>Project pictures</returns>
        //public virtual async Task<ProjectAccount> GetProjectAccountByProjectIdAsync(int projectId, int accountTypeId)
        //{
        //    var query = from pa in _dbContext.ProjectAccounts
        //        join a in _accountRepository.Table on pa.AccountId equals a.Id
        //        where pa.ProjectId == projectId && a.AccountTypeId == accountTypeId
        //        select pa;
        //    var projectAccount = await query.FirstOrDefaultAsync();

        //    return projectAccount;
        //}

        #endregion
    }
}