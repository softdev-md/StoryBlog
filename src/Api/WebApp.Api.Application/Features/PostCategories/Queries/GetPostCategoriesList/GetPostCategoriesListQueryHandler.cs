using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.Api.Application.Contracts.Persistence;

namespace WebApp.Api.Application.Features.PostCategories.Queries.GetPostCategoriesList
{
    public class GetPostCategoriesListQueryHandler : IRequestHandler<GetPostCategoriesListQuery, List<PostCategoryModel>>
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPostCategoriesListQueryHandler> _logger;

        public GetPostCategoriesListQueryHandler(IMapper mapper, IPostCategoryRepository postCategoryRepository, ILogger<GetPostCategoriesListQueryHandler> logger)
        {
            _mapper = mapper;
            _postCategoryRepository = postCategoryRepository;
            _logger = logger;
        }

        public async Task<List<PostCategoryModel>> Handle(GetPostCategoriesListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Post categories list was responsed");

            var allPostCategories = (await _postCategoryRepository.GetAllPostCategoriesAsync(request.ProjectId));
            return _mapper.Map<List<PostCategoryModel>>(allPostCategories);
        }
    }
}
