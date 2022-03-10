using System.Collections.Generic;
using AutoMapper;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using WebApp.Api.Application.Features.PostCategories.Queries.GetPostCategoriesList;
using WebApp.Api.Application.Features.Posts.Commands.CreatePost;
using WebApp.Api.Application.Features.Posts.Commands.UpdatePost;
using WebApp.Api.Application.Features.Posts.Queries.GetPostDetail;
using WebApp.Api.Application.Features.Posts.Queries.GetPostsList;
using WebApp.Api.Application.Features.Projects.Queries.GetProjectsList;
using WebApp.Api.Domain.Entities;

namespace WebApp.Grpc.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(IList<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldTypeConverter<,>));
            CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));

            CreateMap<PostModel, WebApp.Grpc.Protos.PostModel>()
                .ForMember(model => model.CreatedOn, options => options.MapFrom(p => p.CreatedOn.ToUniversalTime().ToTimestamp()))
                .ForMember(model => model.PublishedOn, options => options.MapFrom(p => p.PublishedOn.HasValue? p.PublishedOn.Value.ToUniversalTime().ToTimestamp() : null))
                .ReverseMap();

        }

        #region Converters

        private class ListToRepeatedFieldTypeConverter<TITemSource, TITemDest> : ITypeConverter<IList<TITemSource>, RepeatedField<TITemDest>>
        {
            public RepeatedField<TITemDest> Convert(IList<TITemSource> source, RepeatedField<TITemDest> destination, ResolutionContext context)
            {
                destination = destination ?? new RepeatedField<TITemDest>();
                foreach (var item in source)
                {
                    destination.Add(context.Mapper.Map<TITemDest>(item));
                }
                return destination;
            }
        }

        private class RepeatedFieldToListTypeConverter<TITemSource, TITemDest> : ITypeConverter<RepeatedField<TITemSource>, IList<TITemDest>>
        {
            public IList<TITemDest> Convert(RepeatedField<TITemSource> source, IList<TITemDest> destination, ResolutionContext context)
            {
                destination = destination ?? new List<TITemDest>();
                foreach (var item in source)
                {
                    destination.Add(context.Mapper.Map<TITemDest>(item));
                }
                return destination;
            }
        }

        #endregion
    }
}
