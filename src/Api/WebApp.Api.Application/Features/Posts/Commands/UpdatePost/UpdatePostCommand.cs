using MediatR;

namespace WebApp.Api.Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }

        public int PostCategoryId { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }
    }
}
