using System.Collections.Generic;
using MediatR;

namespace WebApp.Api.Application.Features.PostCategories.Queries.GetPostCategoriesList
{
    public class GetPostCategoriesListQuery : IRequest<List<PostCategoryModel>>
    {
        public int ProjectId { get; set; }
    }
}
