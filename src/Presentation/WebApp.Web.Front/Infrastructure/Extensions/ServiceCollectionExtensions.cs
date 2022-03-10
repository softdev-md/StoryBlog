using System;
using System.Net.Http;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Authentication;
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
        /// <summary>
        /// Adds Store related services and infrastructure.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>A reference to current <see cref="IServiceCollection"/> after operation is completed</returns>
        public static IServiceCollection AddStoreFrontServices(this IServiceCollection services)
        {
            services.AddScoped<IJSService, JSService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<IPostService, PostService>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="HttpClient"/> for <see cref="IConfiguration"/> declared service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHttpHandlers(this IServiceCollection services)
        {
            // If declared, Refit uses custom DelegatingHandler handler for request preprocessing.
            // register custom DelegatingHandler
            services.AddTransient<DefaultHttpMessageHandler>();

            //services.AddTransient<LoggingDelegatingHandler>();
            
            //builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("Api"));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="HttpClient"/> for <see cref="IConfiguration"/> declared service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> collection.</param>
        /// <param name="configuration"></param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRefitClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IPostApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Api:BaseAddress"]))
                .AddHttpMessageHandler<DefaultHttpMessageHandler>();

            services.AddRefitClient<IPostCategoryApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Api:BaseAddress"]))
                .AddHttpMessageHandler<DefaultHttpMessageHandler>();

            services.AddRefitClient<IProjectApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Api:BaseAddress"]))
                .AddHttpMessageHandler<DefaultHttpMessageHandler>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="HttpClient"/> for <see cref="IConfiguration"/> declared service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> collection.</param>
        /// <param name="configuration"></param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<PostProtoService.PostProtoServiceClient>(options =>
            {
                options.Address = new Uri(configuration["Grpc:PostAddress"]);
            }).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler()));
            
            services.AddScoped<PostGrpcService>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="HttpClient"/> for <see cref="IConfiguration"/> declared service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> collection.</param>
        /// <param name="configuration"></param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = "Cookies";
            //        options.DefaultChallengeScheme = "oidc";
            //    })
            //    //.AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            //    .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.Authority = configuration["ServiceUrls:IdentityAPI"];
            //        options.GetClaimsFromUserInfoEndpoint = true;
            //        options.ClientId = "mango";
            //        options.ClientSecret = "secret";
            //        options.ResponseType = "code";
            //        options.ClaimActions.MapJsonKey("role", "role", "role");
            //        options.ClaimActions.MapJsonKey("sub", "sub", "sub");
            //        options.TokenValidationParameters.NameClaimType = "name";
            //        options.TokenValidationParameters.RoleClaimType = "role";
            //        options.Scope.Add("mango");
            //        options.SaveTokens = true;

            //    });

            return services;
        }
    }
}
