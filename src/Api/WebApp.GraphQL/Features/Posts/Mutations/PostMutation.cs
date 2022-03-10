using System;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Domain.Entities;

namespace WebApp.GraphQL.Features.Posts.Mutations
{
    //[ExtendObjectType(Name = "Mutation")]
    public class PostMutation
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostMutation(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<Post> CreatePost(CreatePostInput input)
        {
            var validator = new PostInputValidator();
            var validationResult = await validator.ValidateAsync(input);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
            
            var post = _mapper.Map<Post>(input);
            post.CreatedOn = DateTime.Now;

            var createdPost = await _postRepository.InsertAsync(post);

            return createdPost;
        }

        public async Task<bool> UpdatePost(UpdatePostInput input)
        {
            var postToUpdate = await _postRepository.GetByIdAsync(input.Id);

            if (postToUpdate == null)
            {
                throw new NotFoundException(nameof(Post), input.Id);
            }

            //var validator = new PostInputValidator();
            //var validationResult = await validator.ValidateAsync(input);

            //if (validationResult.Errors.Count > 0)
            //    throw new ValidationException(validationResult);

            _mapper.Map(input, postToUpdate, typeof(UpdatePostInput), typeof(Post));

            await _postRepository.UpdateAsync(postToUpdate);

            return true;
        }

        public async Task<bool> DeletePost(DeletePostInput input)
        {
            var postToDelete = await _postRepository.GetByIdAsync(input.Id);

            if (postToDelete == null)
            {
                throw new NotFoundException(nameof(Post), input.Id);
            }

            await _postRepository.DeleteAsync(postToDelete);

            return true;
        }
    }
}
