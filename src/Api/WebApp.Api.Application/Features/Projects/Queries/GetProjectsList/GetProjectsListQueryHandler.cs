using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApp.Api.Application.Contracts.Persistence;

namespace WebApp.Api.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQueryHandler : IRequestHandler<GetProjectsListQuery, List<ProjectModel>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsListQueryHandler(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectModel>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            var allProjects = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectModel>>(allProjects);
        }
    }
}
