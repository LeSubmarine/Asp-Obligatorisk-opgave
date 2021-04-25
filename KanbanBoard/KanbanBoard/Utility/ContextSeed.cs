using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Models;
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
            await roleManager.CreateAsync(new IdentityRole("UltraAdmin"));
        }

        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "Henrik@gmail.com",
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
                    await userManager.AddToRoleAsync(defaultUser, "UltraAdmin");
                }
            }
        }

        public static async Task SeedIdentityUsersAsync(UserManager<IdentityUser> userManager)
        {
            //Seed Default Users
            if (userManager.Users.Count() < 3)
            {
                List<string> names = new List<string>()
                {
                    "Boina",
                    "Rik",
                    "Lilj",
                    "Frej",
                    "Kirst",
                    "Torsten",
                    "Krejner"
                };
                List<string> roles = new List<string>()
                {
                    "Organizer",
                    "TeamPlayer",
                    "Contributer",
                    "Contributer",
                    "Contributer",
                    "Contributer",
                    "Observer"
                };
                for (int i = 0; i < names.Count; i++)
                {
                    IdentityUser newUser = new IdentityUser(names[i]);
                    await userManager.CreateAsync(newUser);
                    await userManager.AddToRoleAsync(newUser, roles[i]); 
                }
            }
        }

        public static async Task SeedKanbanTasksAsync(KanbanTaskManager kanbanTaskManager)
        {
            //Seed Default Tasks
            if (kanbanTaskManager.Tasks.Count <= 0)
            {
                List<string> descriptions = new List<string>()
                {
                    "GOIS WE NEED TO DO DIS BUGGY DOGNK",
                    "Lammayo dis kinda but not really importante",
                    "LoUl keppa hørh hørh",
                    "Yah yah nah nah you suck also do work lel",
                    "KEPPAHARHAHAHAH",
                    "Loremius Ipsumious Maximale sværger dig"
                };
                List<string> titles = new List<string>()
                {
                    "First thang",
                    "Secondus thong",
                    "Thirduis bling blong",
                    "Sværgg dig det skal laves",
                    "WOw du mente det?",
                    "Ej dig ikke kigge i min seeds.. )':"
                };
                List<string> movement = new List<string>()
                {
                    "To Do",
                    "Doing",
                    "Done",
                    "Done",
                    "To Do",
                    "Doing"
                };
                for (int i = 0; i < 6; i++)
                {
                    await kanbanTaskManager.CreateTaskAsync(new KanbanTask
                    {
                        
                    })
                }
            }
        }
    }
}