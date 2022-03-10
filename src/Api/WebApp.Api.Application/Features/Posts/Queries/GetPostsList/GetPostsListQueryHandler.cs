using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApp.Api.Application.Contracts.Persistence;

namespace WebApp.Api.Application.Features.Posts.Queries.GetPostsList
{
    public class GetPostsListQueryHandler : IRequestHandler<GetPostsListQuery, DataSourceResult<PostModel>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostsListQueryHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<DataSourceResult<PostModel>> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
        {
            var allPosts = await _postRepository.GetAllPostsAsync(request.ProjectId, 
                postCategoryIds: new List<int> { request.CategoryId },
                pageIndex: request.Page, pageSize: request.PageSize);

            var result = new DataSourceResult<PostModel>();
            result.Data = _mapper.Map<List<PostModel>>(allPosts);
            result.Total = allPosts.TotalCount;

            return result; //_mapper.Map<List<PostModel>>(allPosts);
        }
    }
}
