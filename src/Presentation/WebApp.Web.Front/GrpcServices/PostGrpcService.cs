using System;
using System.Threading.Tasks;
using WebApp.Grpc.Protos;

namespace WebApp.Web.Front.GrpcServices
{
    public class PostGrpcService
    {
        private readonly PostProtoService.PostProtoServiceClient _postProtoService;

        public PostGrpcService(PostProtoService.PostProtoServiceClient postProtoService)
        {
            _postProtoService = postProtoService ?? throw new ArgumentNullException(nameof(postProtoService));
        }

        public async Task<DataSourceResult> GetAllPostsAsync(int projectId, int categoryId = 0, string keyword = null, 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var postRequest = new PostRequest() { ProjectId = projectId, CategoryId = categoryId, Keyword = keyword, Page = pageIndex, PageSize = pageSize};

            return await _postProtoService.GetAllPostsAsync(postRequest);
        }
    }
}
