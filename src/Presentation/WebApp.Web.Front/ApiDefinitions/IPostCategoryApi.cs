using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using WebApp.Web.Front.ApiDefinitions.Domain;
using WebApp.Web.Front.Models.Common;

namespace WebApp.Web.Front.ApiDefinitions
{
    /// <summary>
    /// Post Category Api Definition
    /// </summary>
    public interface IPostCategoryApi
    {
        [Get("/api/postcategory")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<List<PostCategory>> GetAllPostCategoriesAsync(int projectId);
    }
}