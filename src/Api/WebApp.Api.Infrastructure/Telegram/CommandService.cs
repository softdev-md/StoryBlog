using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Models;
using WebApp.Api.Infrastructure.Telegram.Commands;

namespace WebApp.Api.Infrastructure.Telegram
{
    public class CommandService : ICommandService
    {
        private readonly List<ITelegramCommand> _commands;

        public CommandService()
        {
            _commands = new List<ITelegramCommand>
            {
                new StartCommand(),
                new GetPostCommand(),
                new RandomPostCommand(),
                new RandomPostCallbackCommand(),
                new PublishPostCommand(),
                new PinPostCallbackCommand(),
                new DefaultCommand()
            };
        }

        public List<ITelegramCommand> Get() => _commands;

        public List<ITelegramCommand> Get<T>() => _commands.Where(c => c is T).ToList();
        
        public ITelegramMessageCommand GetMessageCommand(string commandName)
        {
            return (ITelegramMessageCommand)Get<ITelegramMessageCommand>()
                .FirstOrDefault(c => ((ITelegramMessageCommand)c).Name.Equals(commandName));
        }

        public ITelegramMessageCommand GetMessageCommand(Message message)
        {
            return (ITelegramMessageCommand)Get<ITelegramMessageCommand>()
                .FirstOrDefault(c => ((ITelegramMessageCommand)c).Contains(message));
        }

        public ITelegramCallbackCommand GetCallbackCommand(CallbackQuery callbackQuery) =>
            (ITelegramCallbackCommand)Get<ITelegramCallbackCommand>()
                .FirstOrDefault(c => ((ITelegramCallbackCommand)c).Contains(callbackQuery));
    }
}
