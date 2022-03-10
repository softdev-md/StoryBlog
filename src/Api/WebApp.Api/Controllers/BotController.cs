using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BotController : Controller
    {
        private readonly ITelegramService _telegramService;
        private readonly ICommandService _commandService;
        private readonly ILogger<BotController> _logger;

        public BotController(ITelegramService telegramService, 
            ICommandService commandService,
            ILogger<BotController> logger)
        {
            this._telegramService = telegramService;
            this._commandService = commandService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Started");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message            => BotOnMessageReceived(update.Message!),
                UpdateType.CallbackQuery      => BotOnCallbackQueryReceived(update.CallbackQuery!),
                _                             => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bot update message error");
            }
            
            return Ok();
        }

        private async Task BotOnMessageReceived(Message message)
        {
            var command = _commandService.GetMessageCommand(message);
            if(command != null)
                await command.Execute(message, _telegramService);
            else
            {
                var defaultCommand = _commandService.GetMessageCommand("/default");
                if (defaultCommand != null)
                    await defaultCommand.Execute(message, _telegramService);
            }
        }
        
        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            var command = _commandService.GetCallbackCommand(callbackQuery);
            if(command != null)
                await command.Execute(callbackQuery, _telegramService);
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            _logger.LogInformation($"Unknown update type: {update.Type}");

            return Task.CompletedTask;
        }
    }
}