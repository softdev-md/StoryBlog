using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Application.Features.Posts.Commands.CreatePost;
using WebApp.Api.Application.Features.Posts.Commands.DeletePost;
using WebApp.Api.Application.Features.Posts.Commands.PinPost;
using WebApp.Api.Application.Features.Posts.Commands.PublishPost;
using WebApp.Api.Application.Features.Posts.Commands.UpdatePost;
using WebApp.Api.Application.Features.Posts.Queries.GetPostDetail;
using WebApp.Api.Application.Features.Posts.Queries.GetPostsList;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        private Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        [HttpGet(Name = "GetAllPosts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DataSourceResult<PostModel>>> GetAllPosts([FromQuery] GetPostsListQuery query)
        {
            var dtos = await _mediator.Send(query);
            
            return Ok(dtos);
        }
        
        [HttpGet("{id}", Name = "GetPostById")]
        public async Task<ActionResult<PostDetailModel>> GetPostById(int id)
        {
            var getPostDetailQuery = new GetPostDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getPostDetailQuery));
        }

        [Authorize]
        [HttpPost(Name = "AddPost")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePostCommand createPostCommand)
        {
            var id = await _mediator.Send(createPostCommand);
            return Ok(id);
        }

        [Authorize]
        [HttpPut(Name = "UpdatePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdatePostCommand updatePostCommand)
        {
            await _mediator.Send(updatePostCommand);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deletePostCommand = new DeletePostCommand() { Id = id };
            await _mediator.Send(deletePostCommand);
            return NoContent();
        }
        
        [HttpGet("[action]/{id}", Name = "PinPost")]
        public async Task<ActionResult> Pin(int id)
        {
            var pinPostCommand = new PinPostCommand() { Id = id };
            await _mediator.Send(pinPostCommand);
            return NoContent();
        }

        [HttpGet("[action]/{id}", Name = "PublishPost")]
        public async Task<ActionResult> Publish(int id)
        {
            var publishPostCommand = new PublishPostCommand() { Id = id };
            await _mediator.Send(publishPostCommand);
            return NoContent();
        }
    }
}
