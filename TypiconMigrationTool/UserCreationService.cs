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

        public void CreateRole(string name, string normalizedName)
        {
            Role role = new Role()
            {
                Name = name,
                NormalizedName = normalizedName
            };

            _roleManager.CreateAsync(role).Wait();
        }

        public void CreateUser(User user, string password, params string[] roles)
        {
            var result = _userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            else
            {
                result = _userManager.AddToRolesAsync(user, roles).Result;

                if (!result.Succeeded)
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
