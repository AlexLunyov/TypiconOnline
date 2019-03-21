﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypiconMigrationTool.Core;

namespace TypiconMigrationTool.Core.Migrations
{
    [DbContext(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TypiconOnline.Domain.Books.Easter.EasterItem", b =>
                {
                    b.Property<DateTime>("Date");

                    b.HasKey("Date");

                    b.ToTable("EasterItem");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Katavasia.Katavasia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Katavasia");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Oktoikh.OktoikhDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Definition");

                    b.Property<int>("Ihos");

                    b.HasKey("Id");

                    b.ToTable("OktoikhDay");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.Psalter.Psalm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Psalm");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.TheotokionApp.TheotokionApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Definition");

                    b.Property<int>("Ihos");

                    b.Property<int>("Place");

                    b.HasKey("Id");

                    b.ToTable("TheotokionApp");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Books.WeekDayApp.WeekDayApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Definition");

                    b.HasKey("Id");

                    b.ToTable("WeekDayApp");
                });

            modelBuilder.Entity("TypiconOnline.Domain.DayRuleWorship", b =>
                {
                    b.Property<int>("DayRuleId");

                    b.Property<int>("DayWorshipId");

                    b.HasKey("DayRuleId", "DayWorshipId");

                    b.HasIndex("DayWorshipId");

                    b.ToTable("DayRuleWorship");
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

                    b.Property<string>("Definition");

                    b.Property<bool>("IsCelebrating");

                    b.Property<int>("ParentId");

                    b.Property<bool>("UseFullName");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("DayWorship");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.CommonRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("RuleDefinition");

                    b.Property<int>("TypiconVersionId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconVersionId");

                    b.ToTable("CommonRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.DayRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsAddition");

                    b.Property<string>("ModRuleDefinition");

                    b.Property<string>("RuleDefinition");

                    b.Property<int>("TemplateId");

                    b.Property<int>("TypiconVersionId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("DayRule");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DayRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.DayWorshipsFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ExcludedItem");

                    b.Property<int?>("IncludedItem");

                    b.Property<bool?>("IsCelebrating");

                    b.HasKey("Id");

                    b.ToTable("DayWorshipsFilter");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("DayRuleId");

                    b.Property<int?>("FilterId");

                    b.Property<bool>("IsAddition");

                    b.Property<bool>("IsLastName");

                    b.Property<int>("ModifiedYearId");

                    b.Property<int>("Priority");

                    b.Property<int?>("SignNumber");

                    b.Property<bool>("UseFullName");

                    b.HasKey("Id");

                    b.HasIndex("DayRuleId");

                    b.HasIndex("FilterId");

                    b.HasIndex("ModifiedYearId");

                    b.ToTable("ModifiedRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCalculated");

                    b.Property<int>("TypiconVersionId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("TypiconVersionId");

                    b.ToTable("ModifiedYear");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.OutputForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Definition");

                    b.Property<int>("TypiconId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconId");

                    b.ToTable("OutputForm");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.OutputFormDayWorship", b =>
                {
                    b.Property<int>("OutputFormId");

                    b.Property<int>("DayWorshipId");

                    b.HasKey("OutputFormId", "DayWorshipId");

                    b.HasIndex("DayWorshipId");

                    b.ToTable("OutputFormDayWorship");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.Kathisma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.Property<int?>("TypiconVersionId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconVersionId");

                    b.ToTable("Kathisma");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.PsalmLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EndStihos");

                    b.Property<int?>("PsalmId");

                    b.Property<int?>("SlavaElementId");

                    b.Property<int?>("StartStihos");

                    b.HasKey("Id");

                    b.HasIndex("PsalmId");

                    b.HasIndex("SlavaElementId");

                    b.ToTable("PsalmLink");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.SlavaElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KathismaId");

                    b.HasKey("Id");

                    b.HasIndex("KathismaId");

                    b.ToTable("SlavaElement");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Sign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAddition");

                    b.Property<bool>("IsTemplate");

                    b.Property<string>("ModRuleDefinition");

                    b.Property<int?>("Number");

                    b.Property<int>("Priority");

                    b.Property<string>("RuleDefinition");

                    b.Property<int?>("TemplateId");

                    b.Property<int>("TypiconVersionId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.HasIndex("TypiconVersionId");

                    b.ToTable("Sign");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Typicon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OwnerId");

                    b.Property<int?>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Typicon");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BDate");

                    b.Property<DateTime>("CDate");

                    b.Property<string>("DefaultLanguage");

                    b.Property<DateTime?>("EDate");

                    b.Property<bool>("IsModified");

                    b.Property<int>("TypiconId");

                    b.HasKey("Id");

                    b.HasIndex("TypiconId");

                    b.ToTable("TypiconVersion");
                });

            modelBuilder.Entity("TypiconOnline.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdministrator");

                    b.Property<bool>("IsTextEditor");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TypiconOnline.Domain.UserTypicon", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("TypiconId");

                    b.HasKey("UserId", "TypiconId");

                    b.HasIndex("TypiconId");

                    b.ToTable("UserTypicon");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.MenologyDay", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Days.Day");


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


                    b.HasIndex("TypiconVersionId");

                    b.ToTable("MenologyRule");

                    b.HasDiscriminator().HasValue("MenologyRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TriodionRule", b =>
                {
                    b.HasBaseType("TypiconOnline.Domain.Typicon.DayRule");

                    b.Property<int>("DaysFromEaster");

                    b.HasIndex("TypiconVersionId")
                        .HasName("IX_DayRule_TypiconVersionId1");

                    b.ToTable("TriodionRule");

                    b.HasDiscriminator().HasValue("TriodionRule");
                });

            modelBuilder.Entity("TypiconOnline.Domain.DayRuleWorship", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule", "DayRule")
                        .WithMany("DayRuleWorships")
                        .HasForeignKey("DayRuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Days.DayWorship", "DayWorship")
                        .WithMany()
                        .HasForeignKey("DayWorshipId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.DayWorship", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Days.Day", "Parent")
                        .WithMany("DayWorships")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "WorshipName", b1 =>
                        {
                            b1.Property<int>("DayWorshipId");

                            b1.Property<bool>("IsBold");

                            b1.Property<bool>("IsItalic");

                            b1.Property<bool>("IsRed");

                            b1.ToTable("DayWorship");

                            b1.HasOne("TypiconOnline.Domain.Days.DayWorship")
                                .WithOne("WorshipName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "DayWorshipId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextUnit", "Items", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<string>("Language");

                                    b2.Property<int>("NameId");

                                    b2.Property<string>("Text");

                                    b2.HasIndex("NameId");

                                    b2.ToTable("DayWorshipNameItems");

                                    b2.HasOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled")
                                        .WithMany("Items")
                                        .HasForeignKey("NameId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "WorshipShortName", b1 =>
                        {
                            b1.Property<int>("DayWorshipId");

                            b1.Property<bool>("IsBold");

                            b1.Property<bool>("IsItalic");

                            b1.Property<bool>("IsRed");

                            b1.ToTable("DayWorshipShortName");

                            b1.HasOne("TypiconOnline.Domain.Days.DayWorship")
                                .WithOne("WorshipShortName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "DayWorshipId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextUnit", "Items", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<string>("Language");

                                    b2.Property<int>("NameId");

                                    b2.Property<string>("Text");

                                    b2.HasIndex("NameId");

                                    b2.ToTable("DayWorshipShortNameItems");

                                    b2.HasOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled")
                                        .WithMany("Items")
                                        .HasForeignKey("NameId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.CommonRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion", "TypiconVersion")
                        .WithMany("CommonRules")
                        .HasForeignKey("TypiconVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("TypiconOnline.Domain.Typicon.DayRule", "DayRule")
                        .WithMany()
                        .HasForeignKey("DayRuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.Modifications.DayWorshipsFilter", "Filter")
                        .WithMany()
                        .HasForeignKey("FilterId");

                    b.HasOne("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", "Parent")
                        .WithMany("ModifiedRules")
                        .HasForeignKey("ModifiedYearId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "ShortName", b1 =>
                        {
                            b1.Property<int>("ModifiedRuleId");

                            b1.Property<bool>("IsBold");

                            b1.Property<bool>("IsItalic");

                            b1.Property<bool>("IsRed");

                            b1.ToTable("ModifiedRuleShortName");

                            b1.HasOne("TypiconOnline.Domain.Typicon.Modifications.ModifiedRule")
                                .WithOne("ShortName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemTextStyled", "ModifiedRuleId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextUnit", "Items", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<string>("Language");

                                    b2.Property<int>("NameId");

                                    b2.Property<string>("Text");

                                    b2.HasIndex("NameId");

                                    b2.ToTable("ModifiedRuleShortNameItems");

                                    b2.HasOne("TypiconOnline.Domain.ItemTypes.ItemTextStyled")
                                        .WithMany("Items")
                                        .HasForeignKey("NameId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Modifications.ModifiedYear", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion", "TypiconVersion")
                        .WithMany("ModifiedYears")
                        .HasForeignKey("TypiconVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.OutputForm", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Typicon", "Typicon")
                        .WithMany()
                        .HasForeignKey("TypiconId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.OutputFormDayWorship", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Days.DayWorship", "DayWorship")
                        .WithMany()
                        .HasForeignKey("DayWorshipId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.OutputForm", "OutputForm")
                        .WithMany("OutputFormDayWorships")
                        .HasForeignKey("OutputFormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.Kathisma", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion", "TypiconVersion")
                        .WithMany("Kathismas")
                        .HasForeignKey("TypiconVersionId");

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemText", "NumberName", b1 =>
                        {
                            b1.Property<int>("KathismaId");

                            b1.ToTable("Kathisma");

                            b1.HasOne("TypiconOnline.Domain.Typicon.Psalter.Kathisma")
                                .WithOne("NumberName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemText", "KathismaId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextUnit", "Items", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<string>("Language");

                                    b2.Property<int>("NameId");

                                    b2.Property<string>("Text");

                                    b2.HasIndex("NameId");

                                    b2.ToTable("KathismaNumberNameItems");

                                    b2.HasOne("TypiconOnline.Domain.ItemTypes.ItemText")
                                        .WithMany("Items")
                                        .HasForeignKey("NameId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.PsalmLink", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Books.Psalter.Psalm", "Psalm")
                        .WithMany()
                        .HasForeignKey("PsalmId");

                    b.HasOne("TypiconOnline.Domain.Typicon.Psalter.SlavaElement")
                        .WithMany("PsalmLinks")
                        .HasForeignKey("SlavaElementId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Psalter.SlavaElement", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Psalter.Kathisma")
                        .WithMany("SlavaElements")
                        .HasForeignKey("KathismaId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Sign", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Sign", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion", "TypiconVersion")
                        .WithMany("Signs")
                        .HasForeignKey("TypiconVersionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemText", "SignName", b1 =>
                        {
                            b1.Property<int>("SignId");

                            b1.ToTable("Sign");

                            b1.HasOne("TypiconOnline.Domain.Typicon.Sign")
                                .WithOne("SignName")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemText", "SignId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextUnit", "Items", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<string>("Language");

                                    b2.Property<int>("NameId");

                                    b2.Property<string>("Text");

                                    b2.HasIndex("NameId");

                                    b2.ToTable("SignNameItems");

                                    b2.HasOne("TypiconOnline.Domain.ItemTypes.ItemText")
                                        .WithMany("Items")
                                        .HasForeignKey("NameId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.Typicon", b =>
                {
                    b.HasOne("TypiconOnline.Domain.User", "Owner")
                        .WithMany("OwnedTypicons")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.Typicon.Typicon", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TypiconVersion", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Typicon", "Typicon")
                        .WithMany("Versions")
                        .HasForeignKey("TypiconId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemText", "Name", b1 =>
                        {
                            b1.Property<int>("TypiconVersionId");

                            b1.ToTable("TypiconVersion");

                            b1.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion")
                                .WithOne("Name")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemText", "TypiconVersionId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemTextUnit", "Items", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<string>("Language");

                                    b2.Property<int>("NameId");

                                    b2.Property<string>("Text");

                                    b2.HasIndex("NameId");

                                    b2.ToTable("TypiconVersionNameItems");

                                    b2.HasOne("TypiconOnline.Domain.ItemTypes.ItemText")
                                        .WithMany("Items")
                                        .HasForeignKey("NameId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.UserTypicon", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.Typicon", "Typicon")
                        .WithMany("EditableUserTypicons")
                        .HasForeignKey("TypiconId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TypiconOnline.Domain.User", "User")
                        .WithMany("EditableUserTypicons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TypiconOnline.Domain.Days.MenologyDay", b =>
                {
                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemDate", "Date", b1 =>
                        {
                            b1.Property<int>("MenologyDayId");

                            b1.Property<int>("Day");

                            b1.Property<int>("Month");

                            b1.ToTable("Day");

                            b1.HasOne("TypiconOnline.Domain.Days.MenologyDay")
                                .WithOne("Date")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemDate", "MenologyDayId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemDate", "LeapDate", b1 =>
                        {
                            b1.Property<int>("MenologyDayId");

                            b1.Property<int>("Day");

                            b1.Property<int>("Month");

                            b1.ToTable("Day");

                            b1.HasOne("TypiconOnline.Domain.Days.MenologyDay")
                                .WithOne("LeapDate")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemDate", "MenologyDayId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.MenologyRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion", "TypiconVersion")
                        .WithMany("MenologyRules")
                        .HasForeignKey("TypiconVersionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemDate", "Date", b1 =>
                        {
                            b1.Property<int>("MenologyRuleId");

                            b1.Property<int>("Day");

                            b1.Property<int>("Month");

                            b1.ToTable("DayRule");

                            b1.HasOne("TypiconOnline.Domain.Typicon.MenologyRule")
                                .WithOne("Date")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemDate", "MenologyRuleId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("TypiconOnline.Domain.ItemTypes.ItemDate", "LeapDate", b1 =>
                        {
                            b1.Property<int>("MenologyRuleId");

                            b1.Property<int>("Day");

                            b1.Property<int>("Month");

                            b1.ToTable("DayRule");

                            b1.HasOne("TypiconOnline.Domain.Typicon.MenologyRule")
                                .WithOne("LeapDate")
                                .HasForeignKey("TypiconOnline.Domain.ItemTypes.ItemDate", "MenologyRuleId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TypiconOnline.Domain.Typicon.TriodionRule", b =>
                {
                    b.HasOne("TypiconOnline.Domain.Typicon.TypiconVersion", "TypiconVersion")
                        .WithMany("TriodionRules")
                        .HasForeignKey("TypiconVersionId")
                        .HasConstraintName("FK_DayRule_TypiconVersion_TypiconVersionId1")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
