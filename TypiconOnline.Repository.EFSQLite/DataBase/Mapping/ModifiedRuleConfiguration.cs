using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class ModifiedRuleConfiguration : IEntityTypeConfiguration<ModifiedRule>
    {
        public void Configure(EntityTypeBuilder<ModifiedRule> builder)
        {
            //builder.HasMany(c => c.DayRuleWorships).
            //    WithOne();
            //Map(m =>
            //{
            //    m.ToTable("DayRuleServices");
            //    // Настройка внешних ключей промежуточной таблицы
            //    m.MapLeftKey("DayRuleId");
            //    m.MapRightKey("ServiceDayId");
            //});
            builder.OwnsOne(c => c.Filter/*, k =>
            {
                k.OwnsOne(m => m.ExcludedItem);
            }*/);
        }
    }
}
