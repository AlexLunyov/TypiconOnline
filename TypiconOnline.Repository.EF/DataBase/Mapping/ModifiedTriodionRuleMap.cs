using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class ModifiedTriodionRuleMap : EntityTypeConfiguration<ModifiedTriodionRule>
    {
        public ModifiedTriodionRuleMap()
        {
            HasRequired(c => c.RuleEntity);

            ToTable("ModifiedTriodionRules");
        }
    }
}