using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class MenologyRuleMap : EntityTypeConfiguration<MenologyRule>
    {
        public MenologyRuleMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();
            Property(c => c.Name).HasMaxLength(200);
            HasOptional(e => e.Folder).
                WithMany();

            //Ignore(c => c.Rule);

            Property(c => c.RuleDefinition).HasColumnType("xml");//("NVARCHAR");

            HasRequired(e => e.Owner).
                WithMany();

            HasRequired(e => e.Template).
                WithMany().WillCascadeOnDelete(false);

            HasRequired(c => c.Day).
                WithMany();

            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("MenologyRules");
            });

            //ToTable("MenologyRules");
        }
    }
}