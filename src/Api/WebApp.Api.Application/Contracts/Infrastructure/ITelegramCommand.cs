using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace WebApp.Api.Application.Contracts.Infrastructure
{
    public interface ITelegramCommand
    {
        string Name { get; }
    }
}
