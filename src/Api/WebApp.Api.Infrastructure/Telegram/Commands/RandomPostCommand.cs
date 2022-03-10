using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class RandomPostCommand : ITelegramMessageCommand
    {
        public string Name => @"/random";
        
        public async Task Execute(Message message, ITelegramService telegramService)
        {
            var chatId = message.Chat.Id;
            var post = await EngineContext.Current.Resolve<IPostRepository>().GetPostAsync(1);

            var text = $"{post.Body}\n{post.Tags}\nЕще истории /start\nСлучайные /random";

            var inlineButtons = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("\uD83D\uDCE8 Продолжить", $"/randomCallback:-"),
            };

            if (await telegramService.IsChatMemberAdministratorAsync(message, 293627263, "vsv_md"))
            {
                inlineButtons.AddRange(new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDCCC Закрепить", $"/pin:{post.Id}"),
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDCE8 Опубликовать", $"/publish:{post.Id}")
                });
            }

            var inlineKeyboard = new InlineKeyboardMarkup(new[] { inlineButtons });

            await telegramService.SendMessageAsync(chatId, text, replyMarkup: inlineKeyboard);
        }
    }
}
