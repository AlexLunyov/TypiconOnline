using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class TypiconVersionConfiguration : IEntityTypeConfiguration<TypiconVersion>
    {
        public void Configure(EntityTypeBuilder<TypiconVersion> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.PrevVersion)
                .WithMany()
                .HasForeignKey(c => c.PrevVersionId)
                .IsRequired(false);

            builder.HasMany(e => e.ModifiedYears)
                .WithOne(m => m.TypiconVersion)
                .HasForeignKey(c => c.TypiconVersionId);

            builder.HasMany(c => c.CommonRules)
                .WithOne(d => d.TypiconVersion)
                .HasForeignKey(c => c.TypiconVersionId);

            //builder.HasMany(c => c.MenologyRules)
            //    .WithOne(d => d.TypiconVersion)
            //    .HasForeignKey(c => c.TypiconVersionId);

            //builder.HasMany(c => c.TriodionRules)
            //    .WithOne(d => d.TypiconVersion)
            //    .HasForeignKey(c => c.TypiconVersionId);

            builder.HasMany(c => c.Signs)
                .WithOne(d => d.TypiconVersion)
                .HasForeignKey(c => c.TypiconVersionId);

            builder.HasMany(c => c.Kathismas)
                .WithOne(d => d.TypiconVersion);
        }
    }
}
