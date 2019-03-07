﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class MenologyDayConfiguration : IEntityTypeConfiguration<MenologyDay>
    {
        public void Configure(EntityTypeBuilder<MenologyDay> builder)
        {
            builder.OwnsOne(c => c.Date)
                .Ignore(c => c.Expression);
            builder.OwnsOne(c => c.LeapDate)
                .Ignore(c => c.Expression);

            //builder.Property(c => c.Date.Expression).
            //    HasColumnName("Date").
            //    HasMaxLength(7).
            //    IsRequired(false);

            //builder.Property(c => c.DateB.Expression).
            //    HasColumnName("DateB").
            //    HasMaxLength(7).
            //    IsRequired(false);

            //builder.ToTable("MenologyDays");
        }
    }
}
