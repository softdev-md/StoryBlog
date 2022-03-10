using MediatR;

namespace WebApp.Api.Application.Features.Posts.Queries.GetPostsList
{
    public class GetPostsListQuery : IRequest<DataSourceResult<PostModel>>
    {
        public int ProjectId { get; set; }

        public int CategoryId { get; set; }

        public string Keyword { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
