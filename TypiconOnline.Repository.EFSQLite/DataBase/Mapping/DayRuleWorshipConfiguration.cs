using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class DayRuleWorshipConfiguration : IEntityTypeConfiguration<DayRuleWorship>
    {
        public void Configure(EntityTypeBuilder<DayRuleWorship> builder)
        {
            builder.HasKey(c => new { c.DayRuleId, c.DayWorshipId });
        }
    }
}
