using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class TypiconRuleMap : EntityTypeConfiguration<TypiconRule>
    {
        public TypiconRuleMap()
        {
            HasRequired(e => e.Owner).
                WithMany();
        }
    }
}
