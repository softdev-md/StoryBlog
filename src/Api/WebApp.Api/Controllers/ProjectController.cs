using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Application.Features.Projects.Queries.GetProjectsList;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet(Name = "GetAllProjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ProjectModel>>> GetAllProjects()
        {
            var dtos = await _mediator.Send(new GetProjectsListQuery());
            return Ok(dtos);
        }
    }
}