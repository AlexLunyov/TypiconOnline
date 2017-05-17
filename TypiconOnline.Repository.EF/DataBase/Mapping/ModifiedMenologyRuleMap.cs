using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class ModifiedMenologyRuleMap : EntityTypeConfiguration<ModifiedMenologyRule>
    {
        public ModifiedMenologyRuleMap()
        {
            HasRequired(c => c.RuleEntity);

            ToTable("ModifiedMenologyRules");
        }
    }
}