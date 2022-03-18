using System;
using System.Net.Http;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using WebApp.Grpc.Protos;
using WebApp.Web.Front.ApiDefinitions;
using WebApp.Web.Front.GrpcServices;
using WebApp.Web.Front.Infrastructure.Handlers;
using WebApp.Web.Front.Services;

namespace WebApp.Web.Front.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoreFrontServices(this IServiceCollection services)
        {
            services.AddScoped<IJSService, JSService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<IPostService, PostService>();

            return services;
        }
        
        public static IServiceCollection AddHttpHandlers(this IServiceCollection services)
        {
            // If declared, Refit uses custom DelegatingHandler handler for request preprocessing.
            // register custom DelegatingHandler
            services.AddTransient<DefaultHttpMessageHandler>();
            services.AddTransient<CustomAuthorizationMessageHandler>();

            //services.AddTransient<LoggingDelegatingHandler>();

            //builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("Api"));

            return services;
        }
        
        public static IServiceCollection AddRefitClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IPostApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Api:BaseAddress"]))
                .AddHttpMessageHandler<CustomAuthorizationMessageHandler>()
                .AddHttpMessageHandler<DefaultHttpMessageHandler>();

            services.AddRefitClient<IPostCategoryApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Api:BaseAddress"]))
                .AddHttpMessageHandler<DefaultHttpMessageHandler>();

            services.AddRefitClient<IProjectApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Api:BaseAddress"]))
                .AddHttpMessageHandler<DefaultHttpMessageHandler>();

            return services;
        }
        
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("storyBlogApi", config=> {
                    config.BaseAddress = new Uri(configuration["Api:BaseAddress"]);
                })
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { configuration["Api:BaseAddress"] },
                            scopes: new[] { "storyBlogApi" });

                    return handler;
                });

            return services;
        }
        
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<PostProtoService.PostProtoServiceClient>(options =>
            {
                options.Address = new Uri(configuration["Grpc:PostAddress"]);
            }).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler()));
            
            services.AddScoped<PostGrpcService>();

            return services;
        }
        
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOidcAuthentication(options =>
            {
                configuration.Bind("oidc", options.ProviderOptions);
            });

            return services;
        }
    }
}
