using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Identity
{
    public class Role : EntityBase<int>
    {
        public string Name { get; set; }
        public string SystemName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}