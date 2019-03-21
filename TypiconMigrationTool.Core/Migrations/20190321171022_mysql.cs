using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypiconMigrationTool.Core.Migrations
{
    public partial class mysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Date_Month = table.Column<int>(nullable: true),
                    Date_Day = table.Column<int>(nullable: true),
                    LeapDate_Month = table.Column<int>(nullable: true),
                    LeapDate_Day = table.Column<int>(nullable: true),
                    DaysFromEaster = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayWorshipsFilter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExcludedItem = table.Column<int>(nullable: true),
                    IncludedItem = table.Column<int>(nullable: true),
                    IsCelebrating = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorshipsFilter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasterItem",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasterItem", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Katavasia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katavasia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OktoikhDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Ihos = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OktoikhDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Psalm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psalm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheotokionApp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Place = table.Column<int>(nullable: false),
                    Ihos = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheotokionApp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsAdministrator = table.Column<bool>(nullable: false),
                    IsTextEditor = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDayApp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDayApp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayWorship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    WorshipName_IsBold = table.Column<bool>(nullable: false),
                    WorshipName_IsItalic = table.Column<bool>(nullable: false),
                    WorshipName_IsRed = table.Column<bool>(nullable: false),
                    UseFullName = table.Column<bool>(nullable: false),
                    IsCelebrating = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayWorship_Day_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Typicon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typicon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Typicon_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Typicon_Typicon_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Typicon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayWorshipNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorshipNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayWorshipNameItems_DayWorship_NameId",
                        column: x => x.NameId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayWorshipShortName",
                columns: table => new
                {
                    DayWorshipId = table.Column<int>(nullable: false),
                    IsBold = table.Column<bool>(nullable: false),
                    IsItalic = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorshipShortName", x => x.DayWorshipId);
                    table.ForeignKey(
                        name: "FK_DayWorshipShortName_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputForm_Typicon_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "Typicon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconId = table.Column<int>(nullable: false),
                    DefaultLanguage = table.Column<string>(nullable: true),
                    IsModified = table.Column<bool>(nullable: false),
                    CDate = table.Column<DateTime>(nullable: false),
                    BDate = table.Column<DateTime>(nullable: true),
                    EDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconVersion_Typicon_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "Typicon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTypicon",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TypiconId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypicon", x => new { x.UserId, x.TypiconId });
                    table.ForeignKey(
                        name: "FK_UserTypicon_Typicon_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "Typicon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTypicon_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayWorshipShortNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorshipShortNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayWorshipShortNameItems_DayWorshipShortName_NameId",
                        column: x => x.NameId,
                        principalTable: "DayWorshipShortName",
                        principalColumn: "DayWorshipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputFormDayWorship",
                columns: table => new
                {
                    OutputFormId = table.Column<int>(nullable: false),
                    DayWorshipId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputFormDayWorship", x => new { x.OutputFormId, x.DayWorshipId });
                    table.ForeignKey(
                        name: "FK_OutputFormDayWorship_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutputFormDayWorship_OutputForm_OutputFormId",
                        column: x => x.OutputFormId,
                        principalTable: "OutputForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonRule_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kathisma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kathisma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kathisma_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModifiedYear",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    IsCalculated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedYear_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    ModRuleDefinition = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    IsAddition = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    IsTemplate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sign_Sign_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sign_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconVersionNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconVersionNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconVersionNameItems_TypiconVersion_NameId",
                        column: x => x.NameId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KathismaNumberNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KathismaNumberNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KathismaNumberNameItems_Kathisma_NameId",
                        column: x => x.NameId,
                        principalTable: "Kathisma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlavaElement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KathismaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlavaElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlavaElement_Kathisma_KathismaId",
                        column: x => x.KathismaId,
                        principalTable: "Kathisma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    ModRuleDefinition = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Date_Month = table.Column<int>(nullable: true),
                    Date_Day = table.Column<int>(nullable: true),
                    LeapDate_Month = table.Column<int>(nullable: true),
                    LeapDate_Day = table.Column<int>(nullable: true),
                    DaysFromEaster = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayRule_Sign_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRule_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayRule_TypiconVersion_TypiconVersionId1",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignNameItems_Sign_NameId",
                        column: x => x.NameId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsalmLink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PsalmId = table.Column<int>(nullable: true),
                    StartStihos = table.Column<int>(nullable: true),
                    EndStihos = table.Column<int>(nullable: true),
                    SlavaElementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsalmLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsalmLink_Psalm_PsalmId",
                        column: x => x.PsalmId,
                        principalTable: "Psalm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PsalmLink_SlavaElement_SlavaElementId",
                        column: x => x.SlavaElementId,
                        principalTable: "SlavaElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayRuleWorship",
                columns: table => new
                {
                    DayRuleId = table.Column<int>(nullable: false),
                    DayWorshipId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRuleWorship", x => new { x.DayRuleId, x.DayWorshipId });
                    table.ForeignKey(
                        name: "FK_DayRuleWorship_DayRule_DayRuleId",
                        column: x => x.DayRuleId,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayRuleWorship_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifiedRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModifiedYearId = table.Column<int>(nullable: false),
                    DayRuleId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    IsLastName = table.Column<bool>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false),
                    UseFullName = table.Column<bool>(nullable: false),
                    SignNumber = table.Column<int>(nullable: true),
                    FilterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedRule_DayRule_DayRuleId",
                        column: x => x.DayRuleId,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModifiedRule_DayWorshipsFilter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "DayWorshipsFilter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModifiedRule_ModifiedYear_ModifiedYearId",
                        column: x => x.ModifiedYearId,
                        principalTable: "ModifiedYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifiedRuleShortName",
                columns: table => new
                {
                    ModifiedRuleId = table.Column<int>(nullable: false),
                    IsBold = table.Column<bool>(nullable: false),
                    IsItalic = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRuleShortName", x => x.ModifiedRuleId);
                    table.ForeignKey(
                        name: "FK_ModifiedRuleShortName_ModifiedRule_ModifiedRuleId",
                        column: x => x.ModifiedRuleId,
                        principalTable: "ModifiedRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifiedRuleShortNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRuleShortNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedRuleShortNameItems_ModifiedRuleShortName_NameId",
                        column: x => x.NameId,
                        principalTable: "ModifiedRuleShortName",
                        principalColumn: "ModifiedRuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonRule_TypiconVersionId",
                table: "CommonRule",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_TemplateId",
                table: "DayRule",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_TypiconVersionId",
                table: "DayRule",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_TypiconVersionId1",
                table: "DayRule",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorship_DayWorshipId",
                table: "DayRuleWorship",
                column: "DayWorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorship_ParentId",
                table: "DayWorship",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorshipNameItems_NameId",
                table: "DayWorshipNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorshipShortNameItems_NameId",
                table: "DayWorshipShortNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Kathisma_TypiconVersionId",
                table: "Kathisma",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_KathismaNumberNameItems_NameId",
                table: "KathismaNumberNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRule_DayRuleId",
                table: "ModifiedRule",
                column: "DayRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRule_FilterId",
                table: "ModifiedRule",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRule_ModifiedYearId",
                table: "ModifiedRule",
                column: "ModifiedYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRuleShortNameItems_NameId",
                table: "ModifiedRuleShortNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedYear_TypiconVersionId",
                table: "ModifiedYear",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputForm_TypiconId",
                table: "OutputForm",
                column: "TypiconId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputFormDayWorship_DayWorshipId",
                table: "OutputFormDayWorship",
                column: "DayWorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_PsalmLink_PsalmId",
                table: "PsalmLink",
                column: "PsalmId");

            migrationBuilder.CreateIndex(
                name: "IX_PsalmLink_SlavaElementId",
                table: "PsalmLink",
                column: "SlavaElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TemplateId",
                table: "Sign",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TypiconVersionId",
                table: "Sign",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_SignNameItems_NameId",
                table: "SignNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_SlavaElement_KathismaId",
                table: "SlavaElement",
                column: "KathismaId");

            migrationBuilder.CreateIndex(
                name: "IX_Typicon_OwnerId",
                table: "Typicon",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Typicon_TemplateId",
                table: "Typicon",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersion_TypiconId",
                table: "TypiconVersion",
                column: "TypiconId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersionNameItems_NameId",
                table: "TypiconVersionNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypicon_TypiconId",
                table: "UserTypicon",
                column: "TypiconId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonRule");

            migrationBuilder.DropTable(
                name: "DayRuleWorship");

            migrationBuilder.DropTable(
                name: "DayWorshipNameItems");

            migrationBuilder.DropTable(
                name: "DayWorshipShortNameItems");

            migrationBuilder.DropTable(
                name: "EasterItem");

            migrationBuilder.DropTable(
                name: "Katavasia");

            migrationBuilder.DropTable(
                name: "KathismaNumberNameItems");

            migrationBuilder.DropTable(
                name: "ModifiedRuleShortNameItems");

            migrationBuilder.DropTable(
                name: "OktoikhDay");

            migrationBuilder.DropTable(
                name: "OutputFormDayWorship");

            migrationBuilder.DropTable(
                name: "PsalmLink");

            migrationBuilder.DropTable(
                name: "SignNameItems");

            migrationBuilder.DropTable(
                name: "TheotokionApp");

            migrationBuilder.DropTable(
                name: "TypiconVersionNameItems");

            migrationBuilder.DropTable(
                name: "UserTypicon");

            migrationBuilder.DropTable(
                name: "WeekDayApp");

            migrationBuilder.DropTable(
                name: "DayWorshipShortName");

            migrationBuilder.DropTable(
                name: "ModifiedRuleShortName");

            migrationBuilder.DropTable(
                name: "OutputForm");

            migrationBuilder.DropTable(
                name: "Psalm");

            migrationBuilder.DropTable(
                name: "SlavaElement");

            migrationBuilder.DropTable(
                name: "DayWorship");

            migrationBuilder.DropTable(
                name: "ModifiedRule");

            migrationBuilder.DropTable(
                name: "Kathisma");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "DayRule");

            migrationBuilder.DropTable(
                name: "DayWorshipsFilter");

            migrationBuilder.DropTable(
                name: "ModifiedYear");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "TypiconVersion");

            migrationBuilder.DropTable(
                name: "Typicon");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
