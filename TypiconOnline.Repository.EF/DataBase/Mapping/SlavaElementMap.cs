using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class SlavaElementMap : EntityTypeConfiguration<SlavaElement>
    {
        public SlavaElementMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();
            //Property(c => c.Name).HasMaxLength(200);
            //HasOptional(e => e.Folder).
            //    WithMany();

            //Ignore(c => c.Rule);

            HasMany(c => c.PsalmLinks);

            //HasMany(c => c.DayServices).WithMany().
            //Map(m =>
            //{
            //    m.ToTable("MenologyRuleServices");
            //    // Настройка внешних ключей промежуточной таблицы
            //    m.MapLeftKey("MenologyRuleId");
            //    m.MapRightKey("ServiceDayId");
            //});
            //HasRequired(c => c.Day).
            //    WithMany();

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("MenologyRules");
            //});
        }
    }
}
