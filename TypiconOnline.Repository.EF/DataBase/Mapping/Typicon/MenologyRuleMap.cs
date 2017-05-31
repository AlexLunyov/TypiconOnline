using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping.Typicon
{
    internal class MenologyRuleMap : EntityTypeConfiguration<MenologyRule>
    {
        public MenologyRuleMap()
        {
            HasRequired(c => c.Day).
                WithMany().WillCascadeOnDelete(false);

            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("MenologyRules");
            });

            //ToTable("MenologyRules");
        }
    }
}