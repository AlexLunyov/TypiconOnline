using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class OutputFormDayWorshipConfiguration : IEntityTypeConfiguration<OutputFormDayWorship>
    {
        public void Configure(EntityTypeBuilder<OutputFormDayWorship> builder)
        {
            builder.HasKey(c => new { c.OutputFormId, c.DayWorshipId });
        }
    }
}
