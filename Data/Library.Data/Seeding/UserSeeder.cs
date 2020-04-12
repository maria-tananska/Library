namespace Library.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Library.Common;
    using Library.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await this.SeedUserAsync(userManager);
            await this.AddUserToRoleAsync(userManager, roleManager, dbContext);
        }

        private async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("admin@library.com");
            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        Email = "admin@library.com",
                        EmailConfirmed = true,
                        UserName = "admin@library.com",
                    }, "admin123");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private async Task AddUserToRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext)
        {
            var user = await userManager.FindByEmailAsync("admin@library.com");
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);

            var exist = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exist)
            {
                return;
            }

            var userRole = new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            };
            await dbContext.UserRoles.AddAsync(userRole);

            await dbContext.SaveChangesAsync();
        }
    }
}
