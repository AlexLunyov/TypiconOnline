using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class TypiconRuleMap : EntityTypeConfiguration<TypiconRule>
    {
        public TypiconRuleMap()
        {
            HasKey(c => new { c.Id, c.OwnerId });

            Property(c => c.Name).HasMaxLength(200);
            //HasOptional(e => e.Folder).
            //    WithMany();

            Ignore(c => c.Rule);

            Property(c => c.RuleDefinition).HasColumnType("xml");//("NVARCHAR");

            HasRequired(e => e.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId);

            HasOptional(e => e.Template)
                .WithMany().WillCascadeOnDelete(false);

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("TypiconRules");
            //});

            ToTable("TypiconRules");
        }
    }
}
