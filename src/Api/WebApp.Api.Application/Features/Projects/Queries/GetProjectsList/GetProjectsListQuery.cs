using System.Collections.Generic;
using MediatR;

namespace WebApp.Api.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQuery : IRequest<List<ProjectModel>>
    {

    }
}
