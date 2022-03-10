using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Models;
using WebApp.Api.Infrastructure.Smm;

namespace WebApp.Api.Infrastructure.Telegram.Commands
{
    public class DefaultCommand : ITelegramMessageCommand
    {
        public string Name => @"/default";

        public bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }

        public async Task Execute(Message message, ITelegramService telegramService)
        {
            var text = "Для начала нажмите /start";
            await telegramService.SendMessageAsync(message.Chat.Id, text);
        }

    }
}
