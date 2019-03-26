using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.WebQuery
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public ICollection<RoleViewModel> Roles { get; set; }
    }
}
