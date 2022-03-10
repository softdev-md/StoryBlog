using MediatR;

namespace WebApp.Api.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<CreatePostCommandResponse>
    {
        public int PostCategoryId { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }
    }
}
