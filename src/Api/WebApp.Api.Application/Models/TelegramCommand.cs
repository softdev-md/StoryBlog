using System.Threading.Tasks;
using Telegram.Bot.Types;
using WebApp.Api.Application.Contracts.Infrastructure;

namespace WebApp.Api.Application.Models
{
    public abstract class TelegramCommandh
    {
        public abstract string Name { get; }

        public abstract Task Execute(Update update, ITelegramService telegramService);

        public abstract bool Contains(Message message);
    }
}
