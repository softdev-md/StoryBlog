using MediatR;

namespace WebApp.Api.Application.Features.Posts.Commands.PublishPost
{
    public class PublishPostCommand : IRequest
    {
        public int Id { get; set; }
    }
}
