using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Api.Application;
using WebApp.Api.Infrastructure;
using WebApp.Api.Infrastructure.Extensions;
using WebApp.Api.Persistence;

namespace WebApp.Api
{
    public class Startup
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        #endregion

        #region Ctor

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddApplicationServices();
            services.AddDataServices(_configuration);
            services.AddInfrastructureServices(_configuration);
            
            services.AddSwagger();
            services.AddAppMvc();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder application, IWebHostEnvironment env)
        {
            application.ConfigureRequestPipeline();

            application.UseAppMvc();

            application.UseAppSwagger(env);

            application.UseAppDeveloperExceptionPage(env);
            
            application.StartApplication();
        }

        #endregion
    }
}