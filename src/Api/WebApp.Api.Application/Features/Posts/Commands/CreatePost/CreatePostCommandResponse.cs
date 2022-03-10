using WebApp.Api.Application.Responses;

namespace WebApp.Api.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandResponse : BaseResponse
    {
        public CreatePostCommandResponse() : base()
        {

        }

        public CreatePostDto Post { get; set; }
    }
}
