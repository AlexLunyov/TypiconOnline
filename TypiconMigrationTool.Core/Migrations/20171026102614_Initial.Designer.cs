﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using TypiconMigrationTool.Core;
using TypiconOnline.Domain.Rules;

namespace TypiconMigrationTool.Core.Migrations
{
    [DbContext(typeof(MigrationContext))]
    [Migration("20171026102614_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("TypiconOnline.Domain.Books.Easter.EasterItem", b =>
                {
                    b.Property<DateTime>("Date");

                    b.HasKey("Date");

                    b.ToTable("EasterItem");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Oktoikh.OktoikhDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DayDefinition");

                    b.Property<int>("DayOfWeek");

                    b.Property<int>("Ihos");

                    b.HasKey("Id");

                    b.ToTable("OktoikhDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.TheotokionApp.TheotokionApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<int>("Ihos");

                    b.Property<int>("Place");

                    b.Property<string>("StringDefinition");

                    b.HasKey("Id");

                    b.ToTable("TheotokionApp");
                });

            modelBuilder.Entity("TypiconOnline.Domain.DayRuleWorships", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DayRuleId");

                    b.Property<int?>("DayRuleId1");

                    b.Property<int?>("DayWorshipId");

                    b.HasKey("Id");

                    b.HasIndex("DayRuleId");

                    b.HasIndex("DayRuleId1");

                    b.HasIndex("DayWorshipId");

                    b.ToTable("DayRuleWorships");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Day");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Day");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.DayWorship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DayDefinition");

                    b.Property<int?>("DayRuleId");

                    b.Property<bool>("IsCelebrating");

                    b.Property<int?>("ParentId")
                        .IsRequired();

                    b.Property<bool>("UseFullName");

                    b.Property<int?>("WorshipNameId");

                    b.Property<int?>("WorshipShortNameId");

                    b.HasKey("Id");

                    b.HasIndex("DayRuleId");

                    b.HasIndex("ParentId");

                    b.HasIndex("WorshipNameId");

                    b.HasIndex("WorshipShortNameId");

                    b.ToTable("DayWorship");
                });

            modelBuilder.Entity("TypiconOnline.Domain.ItemTypes.ItemDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Expression");

                    b.HasKey("Id");

                    b.ToTable("ItemDate");
                });

            modelBuilder.Entity("TypiconOnline.Domain.ItemTypes.ItemText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StringExpression");

                    b.HasKey("Id");

                    b.ToTable("ItemText");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.CommonRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("OwnerId")
                        .IsRequired();

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("CommonRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.DayRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsAddition");

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("DayRule");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DayRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsAddition");

                    b.Property<bool>("IsLastName");

                    b.Property<int?>("ModifiedYearId");

                    b.Property<int>("Priority");

                    b.Property<int?>("RuleEntityId");

                    b.Property<string>("ShortName");

                    b.Property<bool>("UseFullName");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedYearId");

                    b.HasIndex("RuleEntityId");

                    b.ToTable("ModifiedRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TypiconEntityId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("ModifiedYear");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Sign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAddition");

                    b.Property<int>("Number");

                    b.Property<int?>("Owner.Id");

                    b.Property<int?>("OwnerId");

                    b.Property<int>("Priority");

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("SignNameId");

                    b.Property<int?>("TemplateId");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("Owner.Id")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.HasIndex("SignNameId");

                    b.HasIndex("TemplateId");

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("Sign");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<int?>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("TypiconEntity");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DefaultLanguage");

                    b.Property<bool>("IsExceptionThrownWhenInvalid");

                    b.Property<int?>("TypiconEntity.Id");

                    b.Property<int?>("TypiconEntityId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconEntity.Id")
                        .IsUnique();

                    b.HasIndex("TypiconEntityId");

                    b.ToTable("TypiconSettings");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.MenologyDay", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Days.Day");

                    b.Property<int?>("DateBId");

                    b.Property<int?>("DateId");

                    b.HasIndex("DateBId");

                    b.HasIndex("DateId");

                    b.ToTable("MenologyDay");

                    b.HasDiscriminator().HasValue("MenologyDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.TriodionDay", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Days.Day");

                    b.Property<int>("DaysFromEaster");

                    b.ToTable("TriodionDays");

                    b.HasDiscriminator().HasValue("TriodionDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.MenologyRule", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Typicon.DayRule");

                    b.Property<int?>("DateBId");

                    b.Property<int?>("DateId");

                    b.Property<int?>("OwnerId");

                    b.HasIndex("DateBId");

                    b.HasIndex("DateId");

                    b.HasIndex("OwnerId");

                    b.ToTable("MenologyRule");

                    b.HasDiscriminator().HasValue("MenologyRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TriodionRule", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Typicon.DayRule");

                    b.Property<int>("DaysFromEaster");

                    b.Property<int?>("OwnerId")
                        .HasColumnName("TriodionRule_OwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("TriodionRule");

                    b.HasDiscriminator().HasValue("TriodionRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.DayRuleWorships", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule")
                        .WithMany("DayRuleWorships")
                        .HasForeignKey("DayRuleId");

                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule", "DayRule")
                        .WithMany()
                        .HasForeignKey("DayRuleId1");

                    b.HasOne("TypiconOnline.Domain.Days.DayWorship", "DayWorship")
                        .WithMany()
                        .HasForeignKey("DayWorshipId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.DayWorship", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule")
                        .WithMany("DayWorships")
                        .HasForeignKey("DayRuleId");

                    b.HasOne("TypiconOnline.Domain.Days.Day", "Parent")
                        .WithMany("DayWorships")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemText", "WorshipName")
                        .WithMany()
                        .HasForeignKey("WorshipNameId");

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemText", "WorshipShortName")
                        .WithMany()
                        .HasForeignKey("WorshipShortNameId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.CommonRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity")
                        .WithMany("CommonRules")
                        .HasForeignKey("TypiconEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.DayRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Sign", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear")
                        .WithMany("ModifiedRules")
                        .HasForeignKey("ModifiedYearId");

                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule", "RuleEntity")
                        .WithMany()
                        .HasForeignKey("RuleEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "TypiconEntity")
                        .WithMany("ModifiedYears")
                        .HasForeignKey("TypiconEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Sign", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconSettings")
                        .WithOne("TemplateSunday")
                        .HasForeignKey("TypiconOnline.Domain.Typicon.Sign", "Owner.Id");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemText", "SignName")
                        .WithMany()
                        .HasForeignKey("SignNameId");

                    b.HasOne("TypiconOnline.Domain.Typicon.Sign", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity")
                        .WithMany("Signs")
                        .HasForeignKey("TypiconEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconEntity", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconSettings", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity")
                        .WithOne("Settings")
                        .HasForeignKey("TypiconOnline.Domain.Typicon.TypiconSettings", "TypiconEntity.Id");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "TypiconEntity")
                        .WithMany()
                        .HasForeignKey("TypiconEntityId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.MenologyDay", b =>
                {
                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "DateB")
                        .WithMany()
                        .HasForeignKey("DateBId");

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "Date")
                        .WithMany()
                        .HasForeignKey("DateId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.MenologyRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "DateB")
                        .WithMany()
                        .HasForeignKey("DateBId");

                    b.HasOne("TypiconOnline.Domain.ItemTypes.ItemDate", "Date")
                        .WithMany()
                        .HasForeignKey("DateId");

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany("MenologyRules")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TriodionRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconEntity", "Owner")
                        .WithMany("TriodionRules")
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
