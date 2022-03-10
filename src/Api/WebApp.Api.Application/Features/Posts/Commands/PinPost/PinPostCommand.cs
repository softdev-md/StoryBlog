using MediatR;

namespace WebApp.Api.Application.Features.Posts.Commands.PinPost
{
    public class PinPostCommand : IRequest
    {
        public int Id { get; set; }
    }
}
