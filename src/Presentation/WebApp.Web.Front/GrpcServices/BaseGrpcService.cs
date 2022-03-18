using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using WebApp.Grpc.Protos;

namespace WebApp.Web.Front.GrpcServices
{
    public class BaseGrpcService
    {
        private readonly IAccessTokenProvider _tokenProvider;

        public BaseGrpcService(IAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected async Task<Metadata> GetAuthorizationHeaderAsync()
        {
            var headers = new Metadata();
            var accessTokenResult = await _tokenProvider.RequestAccessToken();
            if (accessTokenResult.TryGetToken(out var token))
            {
                var accessToken = token.Value;
                headers.Add("Authorization", $"Bearer {accessToken}");
            }

            return headers;
        }
    }
}
