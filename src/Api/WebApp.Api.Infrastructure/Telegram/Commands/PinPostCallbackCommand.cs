using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Infrastructure;
using WebApp.Api.Application.Models;
using WebApp.Api.Infrastructure.Smm;

namespace WebApp.Api.Infrastructure.Telegram.Commands
{
    public class PinPostCallbackCommand : ITelegramCallbackCommand
    {
        public string Name => @"/pin";
        
        public async Task Execute(CallbackQuery callbackQuery, ITelegramService telegramService)
        {
            var dataValues = callbackQuery.Data.Split(':');
            if (dataValues.Length != 2)
                return;

            var postId = dataValues[1];

            //create and send post data
            var postData = new NameValueCollection
            {
                { "postId", postId }
            };

            try
            {
                //var siteContext = EngineContext.Current.Resolve<IWorkContext>();
                //var postPublishUrl = $"{siteContext.CurrentSite.Url}postertask/pinpost";
                //using (var client = new WebClient())
                //{
                //    client.UploadValues(postPublishUrl, postData);
                //}

                await telegramService.AnswerCallbackQueryAsync(
                    callbackQuery.Id, "Пост был успешно закреплен");
            }
            catch (Exception ex)
            {
                var _serviceScopeFactory = EngineContext.Current.Resolve<IServiceScopeFactory>();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    // Resolve
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<PinPostCallbackCommand>>();
                    logger.LogError(ex.Message, ex);
                }

                await telegramService.AnswerCallbackQueryAsync(
                    callbackQuery.Id, "Ошибка публикации поста");
            }
        }
    }
}
