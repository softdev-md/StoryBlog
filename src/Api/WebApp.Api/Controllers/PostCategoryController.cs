using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Application.Features.PostCategories.Queries.GetPostCategoriesList;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : Controller
    {
        private readonly IMediator _mediator;

        public PostCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet(Name = "GetAllPostCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<PostCategoryModel>>> GetAllPostCategories([FromQuery] GetPostCategoriesListQuery query)
        {
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
    }
}