using MediatR;

namespace WebApp.Api.Application.Features.Posts.Queries.GetPostDetail
{
    public class GetPostDetailQuery : IRequest<PostDetailModel>
    {
        public int Id { get; set; }
    }
}
