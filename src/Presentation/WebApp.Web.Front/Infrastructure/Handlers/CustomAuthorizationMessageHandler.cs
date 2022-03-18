using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using Refit;
using WebApp.Web.Front.Infrastructure.Exceptions;

namespace WebApp.Web.Front.Infrastructure.Handlers
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation) :
            base(provider, navigation)
        {
            ConfigureHandler( authorizedUrls: new[] { "https://localhost:44322" }, scopes: new[] { "storyBlogApi" });
        }
    }
}