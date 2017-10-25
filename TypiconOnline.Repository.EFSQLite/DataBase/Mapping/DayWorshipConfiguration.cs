using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class DayWorshipConfiguration : IEntityTypeConfiguration<DayWorship>
    {
        public void Configure(EntityTypeBuilder<DayWorship> builder)
        {
            //builder.OwnsOne(c => c.WorshipName);
            //builder.OwnsOne(c => c.WorshipShortName);

            builder.HasOne(e => e.Parent).
                WithMany();
            //HasRequired(e => e.Parent).
            //    WithMany();

            //builder.ToTable("DayServices");
            //ToTable("DayServices");
        }
    }
}
