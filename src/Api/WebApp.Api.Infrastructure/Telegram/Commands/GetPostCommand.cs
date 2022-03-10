using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Infrastructure;
using WebApp.Api.Application.Models;
using WebApp.Api.Infrastructure.Smm;

namespace WebApp.Api.Infrastructure.Telegram.Commands
{
    public class GetPostCommand : ITelegramCallbackCommand
    {
        public string Name => @"/category";
        
        public async Task Execute(CallbackQuery callbackQuery, ITelegramService telegramService)
        {
            var dataValues = callbackQuery.Data.Split(':');
            if (dataValues.Length != 2)
                return;

            var chatId = callbackQuery.Message.Chat.Id;
            var tags = dataValues[1];

            var post = await EngineContext.Current.Resolve<IPostRepository>().GetPostAsync(1, tags: tags);

            var text = $"{post.Body}\n{post.Tags}\nЕще истории /start\nСлучайные /random";
            
            //await telegramService.AnswerCallbackQueryAsync(
            //    update.CallbackQuery.Id, text);

            var inlineButtons = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("\uD83D\uDCE8 Продолжить", $"/category:{tags}"),
            };
            
            var inlineKeyboard = new InlineKeyboardMarkup(new[] { inlineButtons });

            await telegramService.SendMessageAsync(chatId, text, replyMarkup: inlineKeyboard);
        }
    }
}
