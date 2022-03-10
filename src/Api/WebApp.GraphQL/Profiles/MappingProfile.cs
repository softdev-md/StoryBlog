using AutoMapper;
using WebApp.Api.Domain.Entities;
using WebApp.GraphQL.Features.Posts.Mutations;

namespace WebApp.GraphQL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, CreatePostInput>().ReverseMap();
            CreateMap<Post, UpdatePostInput>().ReverseMap();
        }
    }
}
