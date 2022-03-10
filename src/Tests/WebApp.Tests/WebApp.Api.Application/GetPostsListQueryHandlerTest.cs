using System;
using System.Threading;
using AutoMapper;
using Moq;
using System.Threading.Tasks;
using Shouldly;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Features.Posts.Queries.GetPostsList;
using WebApp.Api.Application.Profiles;
using WebApp.Tests.WebApp.Api.Persistence;
using Xunit;

namespace WebApp.Tests.WebApp.Api.Application
{
    public class GetPostsListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockRepository;

        public GetPostsListQueryHandlerTest()
        {
            _mockRepository = MockPostRepository.GetPostRepository();

            var mapperConfig = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetPostsListTest()
        {
            var handler = new GetPostsListQueryHandler(_mapper, _mockRepository.Object);

            var result = await handler.Handle(new GetPostsListQuery() { PageSize = int.MaxValue}, CancellationToken.None);

            result.ShouldBeOfType<DataSourceResult<PostModel>>();

            result.Total.ShouldBe(2);
        }
    }
}
