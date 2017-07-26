using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class TriodionRuleMap : EntityTypeConfiguration<TriodionRule>
    {
        public TriodionRuleMap()
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

            HasMany(c => c.DayServices).WithMany().
            Map(m =>
            {
                m.ToTable("TriodionRuleServices");
                // Настройка внешних ключей промежуточной таблицы
                m.MapLeftKey("TriodionRuleId");
                m.MapRightKey("ServiceDayId");
            });

            //HasRequired(c => c.Day).
            //    WithMany();

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("TriodionRules");
            //});

            ToTable("TriodionRules");
        }
    }
}