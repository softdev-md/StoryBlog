using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApp.Api.Application.Contracts.Persistence;

namespace WebApp.Api.Application.Features.Posts.Queries.GetPostDetail
{
    public class GetPostDetailQueryHandler : IRequestHandler<GetPostDetailQuery, PostDetailModel>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostDetailQueryHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<PostDetailModel> Handle(GetPostDetailQuery request, CancellationToken cancellationToken)
        {
            var product = await _postRepository.GetByIdAsync(request.Id);
            var productDetailDto = _mapper.Map<PostDetailModel>(product);

            return productDetailDto;
        }
    }
}
