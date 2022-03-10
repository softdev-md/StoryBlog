using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InstagramApiSharp.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Features.Posts.Queries.GetPostsList;
using WebApp.Grpc.Protos;

namespace WebApp.Grpc.Services
{
    public class PostService : PostProtoService.PostProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IMediator mediator, IMapper mapper, ILogger<PostService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<DataSourceResult> GetAllPosts(PostRequest request , ServerCallContext context)
        {
            var query = new GetPostsListQuery()
            {
                ProjectId = request.ProjectId,
                CategoryId = request.CategoryId,
                Keyword = request.Keyword,
                Page = request.Page,
                PageSize = request.PageSize
            };
            var dataSource = await _mediator.Send(query);
            
            var result = new WebApp.Grpc.Protos.DataSourceResult();

            try
            {
                result.Data.AddRange(_mapper.Map<RepeatedField<WebApp.Grpc.Protos.PostModel>>(dataSource.Data));
                result.Total = dataSource.Total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Grpc get all posts error");
            }

            return result;
        }
    }
}
