using AutoMapper;
using WebApp.Api.Application.Features.PostCategories.Queries.GetPostCategoriesList;
using WebApp.Api.Application.Features.Posts.Commands.CreatePost;
using WebApp.Api.Application.Features.Posts.Commands.UpdatePost;
using WebApp.Api.Application.Features.Posts.Queries.GetPostDetail;
using WebApp.Api.Application.Features.Posts.Queries.GetPostsList;
using WebApp.Api.Application.Features.Projects.Queries.GetProjectsList;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectModel>().ReverseMap();
            CreateMap<PostCategory, PostCategoryModel>().ReverseMap();
            CreateMap<Post, PostModel>().ReverseMap();
            CreateMap<Post, CreatePostCommand>().ReverseMap();
            CreateMap<Post, UpdatePostCommand>().ReverseMap();
            CreateMap<Post, PostDetailModel>().ReverseMap();
            CreateMap<Post, CreatePostDto>().ReverseMap();
        }
    }
}
