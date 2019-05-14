using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Identity
{
    public class Role : IdentityRole<int>, IHasId<int> 
    {
        //public string SystemName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}