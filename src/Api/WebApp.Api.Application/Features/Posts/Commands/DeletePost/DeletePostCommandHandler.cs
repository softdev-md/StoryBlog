using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public DeletePostCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var postToDelete = await _postRepository.GetByIdAsync(request.Id);

            if (postToDelete == null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            await _postRepository.DeleteAsync(postToDelete);

            return Unit.Value;
        }
    }
}

