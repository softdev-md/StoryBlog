using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Refit;
using WebApp.Grpc.Protos;
using WebApp.Web.Front.ApiDefinitions;
using WebApp.Web.Front.Components;
using WebApp.Web.Front.GrpcServices;
using WebApp.Web.Front.Infrastructure.Extensions;
using WebApp.Web.Front.Infrastructure.Handlers;
using WebApp.Web.Front.Services;

namespace WebApp.Web.Front
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });
            
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });
            
            builder.Services.AddStoreFrontServices();
            builder.Services.AddHttpHandlers();
            builder.Services.AddRefitClients(builder.Configuration);
            builder.Services.AddGrpcServices(builder.Configuration);

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            
            await builder.Build().RunAsync();
        }
    }
}