using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class TypiconDayRuleMap : EntityTypeConfiguration<TypiconDayRule>
    {
        public TypiconDayRuleMap()
        {
            //HasRequired(c => c.Day).
            //    WithMany();
        }
    }
}