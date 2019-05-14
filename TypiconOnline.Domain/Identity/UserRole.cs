using Microsoft.AspNetCore.Identity;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Identity
{
    public class UserRole: IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}