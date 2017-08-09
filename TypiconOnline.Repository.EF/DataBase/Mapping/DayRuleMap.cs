using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class DayRuleMap : EntityTypeConfiguration<DayRule>
    {
        public DayRuleMap()
        {
            HasMany(c => c.DayServices).WithMany().
            Map(m =>
            {
                m.ToTable("DayRuleServices");
                // Настройка внешних ключей промежуточной таблицы
                m.MapLeftKey("DayRuleId");
                m.MapRightKey("ServiceDayId");
            });
        }
    }
}
