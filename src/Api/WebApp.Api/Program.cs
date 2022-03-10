using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebApp.Logging;

namespace WebApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>();
                });
        }
    }
}
