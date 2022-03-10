using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WebApp.Api.Application.Configuration;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Infrastructure;
using WebApp.Api.Middleware;

namespace WebApp.Api.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.InitServiceProvider(application);
        }

        public static void StartApplication(this IApplicationBuilder application)
        {
            //init telegram bot
            using var scope = application.ApplicationServices.CreateScope();
            EngineContext.Current.Resolve<ITelegramService>(scope).Init().Wait();
        }

        public static void UseAppMvc(this IApplicationBuilder application)
        {
            //application.UseHttpsRedirection();

            application.UseRouting();
            
            application.UseCustomExceptionHandler();

            application.UseCors("Open");

            //app.UseAuthentication();
            //app.UseAuthorization();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void UseAppDeveloperExceptionPage(this IApplicationBuilder application, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                application.UseDeveloperExceptionPage();
        }

        public static void UseAppSwagger(this IApplicationBuilder application, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEB Application API v1"));
            }
        }
    }
}