using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApp.Api.Persistence
{
    public class DbContextFactory: IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();

            optionsBuilder.UseSqlServer("Host=localhost;Port=5432;Database=IMS;Username=postgres;Password=TapRm2021!");

            return new ApiDbContext(optionsBuilder.Options);
        }
    }
}
