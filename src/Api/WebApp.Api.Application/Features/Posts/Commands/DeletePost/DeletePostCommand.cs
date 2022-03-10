using MediatR;

namespace WebApp.Api.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
    }
}
