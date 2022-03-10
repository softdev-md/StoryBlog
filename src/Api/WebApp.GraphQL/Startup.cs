using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Application;
using WebApp.Api.Application.Caching;
using WebApp.Api.Persistence;
using WebApp.GraphQL.Features.Posts;
using WebApp.GraphQL.Features.Posts.Mutations;
using WebApp.GraphQL.Features.Posts.Queries;

namespace WebApp.GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //memory cache
            services.AddMemoryCache();
            
            services.AddSingleton<ILocker, MemoryCacheManager>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddDataServices(Configuration);

            services.AddGraphQLServer()
                .AddQueryType<PostQuery>()
                .AddMutationType<PostMutation>()
                .AddProjections().AddFiltering().AddSorting();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/graphql");
                endpoints.MapControllers();
            });
        }
    }
}
