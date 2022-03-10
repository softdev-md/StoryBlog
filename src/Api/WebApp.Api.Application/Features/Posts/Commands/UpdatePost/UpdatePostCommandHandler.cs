using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var postToUpdate = await _postRepository.GetByIdAsync(request.Id);

            if (postToUpdate == null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            var validator = new UpdatePostCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, postToUpdate, typeof(UpdatePostCommand), typeof(Post));

            await _postRepository.UpdateAsync(postToUpdate);

            return Unit.Value;
        }
    }
}