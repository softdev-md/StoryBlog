using System.Drawing;
using System.Net.Mime;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WebApp.Api.Application.Contracts.Infrastructure
{
    /// <summary>
    /// Telegram service interface
    /// </summary>
    public interface ITelegramService
    {
        /// <summary>
        /// Initialize
        /// </summary>
        Task Init();

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="chatId">Chat Id</param>
        /// <param name="text">Text message</param>
        /// <param name="parseMode"></param>
        /// <param name="disableWebPagePreview"></param>
        /// <param name="disableNotification"></param>
        /// <param name="replyToMessageId"></param>
        /// <param name="replyMarkup"></param>
        Task<Message> SendMessageAsync(ChatId chatId,
            string text,
            ParseMode parseMode = ParseMode.Markdown,
            bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null);

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="chatId">Chat Id</param>
        /// <param name="media">Image</param>
        /// <param name="parseMode"></param>
        /// <param name="disableWebPagePreview"></param>
        /// <param name="disableNotification"></param>
        /// <param name="replyToMessageId"></param>
        /// <param name="replyMarkup"></param>
        Task<Message> SendPhotoAsync(ChatId chatId, 
            Image media,
            ParseMode parseMode = ParseMode.Markdown,
            bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null);

        /// <summary>
        /// Pin chat Message
        /// </summary>
        /// <param name="chatId">Chat Id</param>
        /// <param name="messageId">Message id</param>
        /// <param name="disableNotification"></param>
        Task PinChatMessageAsync(ChatId chatId,
            int messageId,
            bool disableNotification = false);

        /// <summary>
        /// Answer callback query
        /// </summary>
        /// <param name="callbackQueryId">Chat Id</param>
        /// <param name="text">Text message</param>
        /// <param name="showAlert"></param>
        /// <param name="url"></param>
        /// <param name="cacheTime"></param>
        Task AnswerCallbackQueryAsync(string callbackQueryId,
            string text = null,
            bool showAlert = false,
            string url = null,
            int cacheTime = 0);

        Task<bool> IsChatMemberAdministratorAsync(Message message, int userId = 0, string username = null);

        Task<ChatMember> GetChatMemberAsync(Message message);
    }
}
