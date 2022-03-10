using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using WebApp.Api.Application.Configuration;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;

namespace WebApp.Api.Infrastructure.Smm
{
    /// <summary>
    /// Telegram service
    /// </summary>
    public class TelegramService : ITelegramService
    {
        #region Fields

        private ITelegramBotClient _telegramBotClient;
        
        private readonly IOptions<BotConfig> _botConfig;

        #endregion

        #region Ctor

        public TelegramService(IOptions<BotConfig> botConfig)
        {
            this._botConfig = botConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize
        /// </summary>
        public async Task Init()
        {
            _telegramBotClient = new TelegramBotClient(_botConfig.Value.Token);

            //web hook
            if(!string.IsNullOrEmpty(_botConfig.Value.Url))
            {
                var hook = $"{_botConfig.Value.Url}api/message/update";
                await _telegramBotClient.SetWebhookAsync(hook);
            }
        }

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
        public async Task<Message> SendMessageAsync(ChatId chatId, 
            string text,
            ParseMode parseMode = ParseMode.Markdown,
            bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
        {
            var message = await _telegramBotClient.SendTextMessageAsync(chatId, text, 
                parseMode: parseMode, 
                disableWebPagePreview: disableWebPagePreview, 
                disableNotification: disableNotification, 
                replyToMessageId: replyToMessageId, replyMarkup:replyMarkup);
            return message;
        }

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
        public async Task<Message> SendPhotoAsync(ChatId chatId, 
            Image media,
            ParseMode parseMode = ParseMode.Markdown,
            bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
        {
            await using var memoryStream = new MemoryStream();
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

            media.Save(memoryStream, codec, encoderParameters);

            var photo = new InputOnlineFile(memoryStream, "test.jpeg");

            var message = await _telegramBotClient.SendPhotoAsync(chatId, photo);
            return message;
        }

        /// <summary>
        /// Pin chat Message
        /// </summary>
        /// <param name="chatId">Chat Id</param>
        /// <param name="messageId">Message id</param>
        /// <param name="disableNotification"></param>
        public async Task PinChatMessageAsync(ChatId chatId,
            int messageId,
            bool disableNotification = false)
        {
            await _telegramBotClient.PinChatMessageAsync(chatId, messageId, disableNotification);
        }

        /// <summary>
        /// Answer callback query
        /// </summary>
        /// <param name="callbackQueryId">Chat Id</param>
        /// <param name="text">Text message</param>
        /// <param name="showAlert"></param>
        /// <param name="url"></param>
        /// <param name="cacheTime"></param>
        public async Task AnswerCallbackQueryAsync(string callbackQueryId,
            string text = null,
            bool showAlert = false,
            string url = null,
            int cacheTime = 0)
        {
            await _telegramBotClient.AnswerCallbackQueryAsync(callbackQueryId, text, showAlert, url, cacheTime);
        }
        
        public async Task<bool> IsChatMemberAdministratorAsync(Message message, int userId = 0, string username = null)
        {
            var member = await _telegramBotClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);

            if (userId > 0 || !string.IsNullOrEmpty(username))
                return (member.User.Id == userId) || (member.User.Username == username);

            return (member.Status == ChatMemberStatus.Creator) || (member.Status == ChatMemberStatus.Administrator);
        }

        public async Task<ChatMember> GetChatMemberAsync(Message message)
        {
            return await _telegramBotClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
        }
        
        #endregion
    }
}
