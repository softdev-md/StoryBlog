using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Models;
using WebApp.Api.Infrastructure.Smm;

namespace WebApp.Api.Infrastructure.Telegram.Commands
{
    public class StartCommand : ITelegramMessageCommand
    {
        public string Name => @"/start";
        
        public async Task Execute(Message message, ITelegramService telegramService)
        {
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDC8C Любовь", "/category:#любовь"),
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDCBC Работа", "/category:#работа")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDCB0 Деньги", "/category:#деньги"),
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDC8F Отношения", "/category:#отношения")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDC66 Дети", "/category:#дети"),
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDC8A Здоровье", "/category:#здоровье"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDFEB Учеба", "/category:#учеба"),
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDC68 Друзья", "/category:#друзья"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("\uD83D\uDCE8 Разное", "/category:#failstory")
                }
            });

            await telegramService.SendMessageAsync(
                message.Chat.Id,
                "Выберите категорию",
                replyMarkup: inlineKeyboard);
        }
    }
}
