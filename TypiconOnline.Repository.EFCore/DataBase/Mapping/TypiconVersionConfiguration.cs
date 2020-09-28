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

            builder.OwnsOne(c => c.Name, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("NameId");
                    a.WithOwner().HasForeignKey("NameId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                    a.ToTable("TypiconNameItems");
                })
                .Ignore("Discriminator");
                //.ToTable("TypiconVersionName");
            });

            builder.OwnsOne(c => c.Description, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("DescriptionId");
                    a.WithOwner().HasForeignKey("DescriptionId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                    a.ToTable("TypiconDescriptionItems");
                })
                .Ignore("Discriminator");
                //.ToTable("TypiconVersionName");
            });

            builder.HasOne(c => c.PrevVersion)
                .WithMany()
                .HasForeignKey(c => c.PrevVersionId)
                .OnDelete(DeleteBehavior.SetNull)
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

            builder.HasMany(c => c.TypiconVariables)
                .WithOne(d => d.TypiconVersion)
                .HasForeignKey(c => c.TypiconVersionId);

            builder.HasMany(c => c.Kathismas)
                .WithOne(d => d.TypiconVersion);

            //builder.HasMany(c => c.PrintDayTemplates)
            //   .WithOne(d => d.TypiconVersion)
            //   .HasForeignKey(d => d.TypiconVersionId);

            //builder.Property<int>("PrintDayDefaultTemplateId");

            builder.HasOne(c => c.PrintDayDefaultTemplate)
                .WithOne()
                .HasForeignKey<TypiconVersion>("PrintDayDefaultTemplateId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.ScheduleSettings)
                .WithOne(d => d.TypiconVersion)
                .HasForeignKey<ScheduleSettings>(e => e.TypiconVersionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
