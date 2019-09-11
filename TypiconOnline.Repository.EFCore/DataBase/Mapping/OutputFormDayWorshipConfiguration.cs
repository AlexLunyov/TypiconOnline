using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Output;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class OutputFormDayWorshipConfiguration : IEntityTypeConfiguration<OutputDayWorship>
    {
        public void Configure(EntityTypeBuilder<OutputDayWorship> builder)
        {
            builder.HasKey(c => new { c.OutputDayId, c.DayWorshipId });
        }
    }
}
