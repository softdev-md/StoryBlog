using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Api.Application.Caching;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Persistence.Repositories;

namespace WebApp.Api.Persistence
{
    public static class DataServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DbConnectionString"))
               .EnableSensitiveDataLogging()
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            
            return services;
        }
    }
}
