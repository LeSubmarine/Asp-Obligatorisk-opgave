using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KanbanBoard.Utility
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Organizer"));
            await roleManager.CreateAsync(new IdentityRole("Team Player"));
            await roleManager.CreateAsync(new IdentityRole("Contributor"));
            await roleManager.CreateAsync(new IdentityRole("Observer"));
        }
        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "Henrik",
                Email = "Henrik@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, "Organizer");
                    await userManager.AddToRoleAsync(defaultUser, "Team Player");
                    await userManager.AddToRoleAsync(defaultUser, "Contributor");
                    await userManager.AddToRoleAsync(defaultUser, "Observer");
                }

            }
        }
    }
}