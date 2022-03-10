using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using WebApp.Api.Application.Models;

namespace WebApp.Api.Application.Contracts.Infrastructure
{
    public interface ICommandService
    {
        List<ITelegramCommand> Get();

        List<ITelegramCommand> Get<T>();

        ITelegramMessageCommand GetMessageCommand(string commandName);

        ITelegramMessageCommand GetMessageCommand(Message message);

        ITelegramCallbackCommand GetCallbackCommand(CallbackQuery callbackQuery);
    }
}
