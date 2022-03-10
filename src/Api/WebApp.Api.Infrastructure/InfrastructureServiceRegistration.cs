using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Api.Application.Configuration;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Infrastructure.Smm;
using WebApp.Api.Infrastructure.Telegram;

namespace WebApp.Api.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BotConfig>(configuration.GetSection("Bot"));

            services.AddSingleton<ITelegramService, TelegramService>();
            services.AddTransient<IInstagramService, InstagramService>();

            services.AddSingleton<ICommandService, CommandService>();
            
            return services;
        }
    }
}
