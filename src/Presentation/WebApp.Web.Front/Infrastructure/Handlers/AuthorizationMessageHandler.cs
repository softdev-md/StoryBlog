using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Refit;
using WebApp.Web.Front.Infrastructure.Exceptions;

namespace WebApp.Web.Front.Infrastructure.Handlers
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private string JWT_TOKEN = "";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
        {
            HttpRequestHeaders headers = request.Headers;
            AuthenticationHeaderValue authHeader = headers.Authorization;
            if (authHeader != null)
                headers.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, JWT_TOKEN);

            return await base.SendAsync(request, cancelToken);
        }
    }
}
