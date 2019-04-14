using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Identity
{
    public class Role : ValueObjectBase, IHasId<int> 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}