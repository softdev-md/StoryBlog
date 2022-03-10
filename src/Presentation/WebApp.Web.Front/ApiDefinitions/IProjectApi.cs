using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Models.Common;

namespace WebApp.Web.Front.ApiDefinitions
{
    /// <summary>
    /// Project Api Definition
    /// </summary>
    public interface IProjectApi
    {
        [Get("/api/project")]
        Task<List<Project>> GetAllProjectsAsync();

        //[Get("/api/project/{systemName}")]
        //Task<Project> GetProjectBySystemName(string systemName);
    }
}