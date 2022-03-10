using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Shouldly;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Application.Features.Posts.Commands.CreatePost;
using WebApp.Api.Application.Features.Posts.Queries.GetPostsList;
using WebApp.Api.Application.Profiles;
using WebApp.Tests.WebApp.Api.Persistence;
using Xunit;

namespace WebApp.Tests.WebApp.Api.Application
{
    public class CreatePostCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockRepository;
        private CreatePostCommand _requestCommand;

        public CreatePostCommandHandlerTest()
        {
            _mockRepository = MockPostRepository.GetPostRepository();

            var mapperConfig = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();

            _requestCommand = new CreatePostCommand()
            {
                Body = "Test content",
                Author = "John"
            };
        }

        [Fact]
        public async Task ValidPostCreatedTest()
        {
            var handler = new CreatePostCommandHandler(_mapper, _mockRepository.Object);
            _requestCommand.PostCategoryId = 1;
            
            var result = await handler.Handle(_requestCommand, CancellationToken.None);
            
            var list = await _mockRepository.Object.GetAllPostsAsync();
            
            result.ShouldBeOfType<CreatePostCommandResponse>();
            
            list.Count.ShouldBe(3);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task InValidPostCreatedTest()
        {
            var handler = new CreatePostCommandHandler(_mapper, _mockRepository.Object);
            _requestCommand.PostCategoryId = -1;
            
            var ex = await Should.ThrowAsync<ValidationException>(async () =>
            {
                await handler.Handle(_requestCommand, CancellationToken.None);
            });
            
            var list = await _mockRepository.Object.GetAllPostsAsync();

            list.Count.ShouldBe(2);

            ex.ShouldNotBeNull();
        }
    }
}
