using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;

namespace TypiconMigrationTool.Typicon
{
    public class UserCreationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserCreationService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateRole(string name, string normalizedName)
        {
            Role role = new Role()
            {
                Name = name,
                NormalizedName = normalizedName
            };

            await _roleManager.CreateAsync(role);
        }

        public async Task CreateUser(User user, string password, params string[] roles)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            else
            {
                result = await _userManager.AddToRolesAsync(user, roles);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    Console.WriteLine("Done.");
                }
            }
        }
    }
}
