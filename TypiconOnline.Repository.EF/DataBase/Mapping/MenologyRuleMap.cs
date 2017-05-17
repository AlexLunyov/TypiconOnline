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
            //HasKey<int>(c => c.Id);
            //Property(c => c.Id).IsRequired();

            //Property(c => c.Name).HasMaxLength(200).IsRequired();

            //HasRequired(e => e.Folder).
            //    WithMany();

            //Ignore(c => c.Rule);

            //Property(c => c.RuleDefinition).HasColumnType("NVARCHAR");

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("MenologyRules");
            //});

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("MenologyRules");
            //});

            //HasRequired(c => c.Day).
            //    WithMany().WillCascadeOnDelete(false);

            //HasRequired(c => c.Template).
            //    WithMany().WillCascadeOnDelete(false);

            ToTable("MenologyRules");
        }
    }
}