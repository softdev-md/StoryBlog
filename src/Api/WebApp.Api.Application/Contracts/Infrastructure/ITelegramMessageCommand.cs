using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebApp.Api.Application.Contracts.Infrastructure
{
    public interface ITelegramMessageCommand : ITelegramCommand
    {
        Task Execute(Message message, ITelegramService telegramService);

        bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }
    }
}