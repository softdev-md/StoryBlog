using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Domain.Entities;
using WebApp.Api.Persistence;

namespace WebApp.GraphQL.Features.Posts.Queries
{
    public class PostQuery
    {
        [UseProjection]
        //[UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> GetPosts([Service] IPostRepository repository) => repository.TableNoTracking;

        public async Task<Post> GetPostById(int id, [Service] IPostRepository repository) => await repository.GetByIdAsync(id);
    }
}
