using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var createPostCommandResponse = new CreatePostCommandResponse();

            var validator = new CreatePostCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            if (!validationResult.IsValid)
            {
                createPostCommandResponse.Success = false;
                createPostCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createPostCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (createPostCommandResponse.Success)
            {
                var post = _mapper.Map<Post>(request);
                await _postRepository.InsertAsync(post);
                createPostCommandResponse.Post = _mapper.Map<CreatePostDto>(post);
            }

            return createPostCommandResponse;
        }
    }
}
