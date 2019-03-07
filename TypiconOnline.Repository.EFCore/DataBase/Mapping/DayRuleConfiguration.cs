﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class DayRuleConfiguration : IEntityTypeConfiguration<DayRule>
    {
        public void Configure(EntityTypeBuilder<DayRule> builder)
        {
            //builder.HasOne(c => c.TypiconVersion)
            //    .WithMany()
            //    .HasForeignKey(c => c.TypiconVersionId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(c => c.DayRuleWorships).
            //    WithOne().HasForeignKey(c => c.DayRuleId);

            builder.Ignore(c => c.DayWorships);

            builder.HasOne(e => e.Template)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(c => c.TemplateId)
                .IsRequired(true)
                ;
        }
    }
}
