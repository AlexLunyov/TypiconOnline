using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class ModifiedRuleMap : EntityTypeConfiguration<ModifiedRule>
    {
        public ModifiedRuleMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            Ignore(c => c.DayWorships);

            //HasRequired(c => c.RuleEntity).WithMany().WillCascadeOnDelete(false);

            ToTable("ModifiedRules");
        }
    }
}