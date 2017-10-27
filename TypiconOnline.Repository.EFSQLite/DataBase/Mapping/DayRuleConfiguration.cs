using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class DayRuleConfiguration : IEntityTypeConfiguration<DayRule>
    {
        public void Configure(EntityTypeBuilder<DayRule> builder)
        {
            builder.HasMany(c => c.DayRuleWorships).
                WithOne().HasForeignKey(c => c.DayRuleId);
        }
    }
}
