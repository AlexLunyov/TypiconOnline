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
        private readonly UserManager<User> _userManager;

        public UserCreationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, "Test@123");

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            else
            {
                result = await _userManager.AddToRolesAsync(user, new string[] { "admin", "editor", "typesetter" });

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
