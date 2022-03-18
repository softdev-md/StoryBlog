using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Identity.Persistence.Models;

namespace WebApp.Identity.Persistence.Seed
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = "SoftDev",
                LastName = "MD",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
                await userManager.CreateAsync(applicationUser, "admin");
        }
    }
}
