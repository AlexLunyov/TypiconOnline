using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Identity
{
    public class UserRole : EntityBase<int>
    {
        
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}