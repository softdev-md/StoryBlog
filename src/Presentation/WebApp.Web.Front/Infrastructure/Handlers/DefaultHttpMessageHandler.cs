using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Refit;
using WebApp.Web.Front.Infrastructure.Exceptions;

namespace WebApp.Web.Front.Infrastructure.Handlers
{
    public class DefaultHttpMessageHandler : DelegatingHandler
    {
        private readonly ILogger<DefaultHttpMessageHandler> _logger;

        public DefaultHttpMessageHandler(ILogger<DefaultHttpMessageHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            static void ReThrow(string message) => throw new HttpException(HttpStatusCode.InternalServerError, message);

            try
            {
                return await base.SendAsync(request, cancellationToken);
            }
            catch (ValidationApiException validationException)
            {
                var contentErrors = validationException!.Content?.Errors;

                if (contentErrors != null)
                {
                    foreach (var (key, value) in contentErrors)
                    {
                        _logger.LogError(1, validationException.InnerException,
                            new string(value.SelectMany(v => v).ToArray()), Array.Empty<object>());
                    }
                }

                ReThrow(validationException.Message);
            }
            catch (ApiException apiException)
            {
                _logger.LogError(2, apiException, apiException.ReasonPhrase, Array.Empty<object>());

                ReThrow(apiException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(3, e, "Unhandled Http Api Call exception occurred", Array.Empty<object>());

                ReThrow(e.Message);
            }

            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }
    }
}
