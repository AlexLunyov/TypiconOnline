using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class TypiconRuleMap : EntityTypeConfiguration<TypiconRule>
    {
        public TypiconRuleMap()
        {
            HasOptional(e => e.Owner).
                WithMany();
        }
    }
}
