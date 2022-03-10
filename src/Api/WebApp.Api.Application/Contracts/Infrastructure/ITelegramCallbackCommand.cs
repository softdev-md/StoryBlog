using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace WebApp.Api.Application.Contracts.Infrastructure
{
    public interface ITelegramCallbackCommand : ITelegramCommand
    {
        Task Execute(CallbackQuery callbackQuery, ITelegramService telegramService);

        bool Contains(CallbackQuery callbackQuery)
        {
            var dataValues = callbackQuery.Data.Split(':');
            if (dataValues.Length == 2)
                return Name == dataValues.First();

            return false;
        }
    }
}
