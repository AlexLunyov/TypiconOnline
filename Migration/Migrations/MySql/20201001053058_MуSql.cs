using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations.MySql
{
    public partial class MуSql : Migration
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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 127, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 127, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 127, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 127, nullable: true),
                    Email = table.Column<string>(maxLength: 127, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 127, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 127, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    WorshipName_IsBold = table.Column<bool>(nullable: true),
                    WorshipName_IsItalic = table.Column<bool>(nullable: true),
                    WorshipName_IsRed = table.Column<bool>(nullable: true),
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
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SystemName = table.Column<string>(nullable: true),
                    DefaultLanguage = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconEntity_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypiconEntity_TypiconEntity_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 127, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 127, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 127, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayWorshipNameItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorshipNameItems", x => new { x.NameId, x.Language });
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
                    DayWorshipId = table.Column<int>(nullable: false)
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
                name: "OutputDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputDay_TypiconEntity_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SystemName = table.Column<string>(nullable: true),
                    DefaultLanguage = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    ResultMesasge = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconClaim_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypiconClaim_TypiconEntity_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_UserTypicon_TypiconEntity_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTypicon_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayWorshipShortNameItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorshipShortNameItems", x => new { x.NameId, x.Language });
                    table.ForeignKey(
                        name: "FK_DayWorshipShortNameItems_DayWorshipShortName_NameId",
                        column: x => x.NameId,
                        principalTable: "DayWorshipShortName",
                        principalColumn: "DayWorshipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputDayWorship",
                columns: table => new
                {
                    OutputDayId = table.Column<int>(nullable: false),
                    DayWorshipId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputDayWorship", x => new { x.OutputDayId, x.DayWorshipId });
                    table.ForeignKey(
                        name: "FK_OutputDayWorship_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutputDayWorship_OutputDay_OutputDayId",
                        column: x => x.OutputDayId,
                        principalTable: "OutputDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputWorship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OutputDayId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Time = table.Column<string>(nullable: true),
                    Name_IsBold = table.Column<bool>(nullable: true),
                    Name_IsItalic = table.Column<bool>(nullable: true),
                    Name_IsRed = table.Column<bool>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputWorship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputWorship_OutputDay_OutputDayId",
                        column: x => x.OutputDayId,
                        principalTable: "OutputDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconClaimDescriptionItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconClaimDescriptionItems", x => new { x.DescriptionId, x.Language });
                    table.ForeignKey(
                        name: "FK_TypiconClaimDescriptionItems_TypiconClaim_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "TypiconClaim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconClaimNameItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconClaimNameItems", x => new { x.NameId, x.Language });
                    table.ForeignKey(
                        name: "FK_TypiconClaimNameItems_TypiconClaim_NameId",
                        column: x => x.NameId,
                        principalTable: "TypiconClaim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputWorshipAddName",
                columns: table => new
                {
                    OutputWorshipId = table.Column<int>(nullable: false),
                    IsBold = table.Column<bool>(nullable: false),
                    IsItalic = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputWorshipAddName", x => x.OutputWorshipId);
                    table.ForeignKey(
                        name: "FK_OutputWorshipAddName_OutputWorship_OutputWorshipId",
                        column: x => x.OutputWorshipId,
                        principalTable: "OutputWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputWorshipNameItems",
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
                    table.PrimaryKey("PK_OutputWorshipNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputWorshipNameItems_OutputWorship_NameId",
                        column: x => x.NameId,
                        principalTable: "OutputWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputWorshipAddNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    AddNameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputWorshipAddNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputWorshipAddNameItems_OutputWorshipAddName_AddNameId",
                        column: x => x.AddNameId,
                        principalTable: "OutputWorshipAddName",
                        principalColumn: "OutputWorshipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonRuleVariables",
                columns: table => new
                {
                    VariableId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonRuleVariables", x => new { x.VariableId, x.EntityId });
                });

            migrationBuilder.CreateTable(
                name: "DayRuleWorship",
                columns: table => new
                {
                    DayRuleId = table.Column<int>(nullable: false),
                    DayWorshipId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRuleWorship", x => new { x.DayRuleId, x.DayWorshipId });
                    table.ForeignKey(
                        name: "FK_DayRuleWorship_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenologyRulePrintLinks",
                columns: table => new
                {
                    TemplateId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenologyRulePrintLinks", x => new { x.TemplateId, x.EntityId });
                });

            migrationBuilder.CreateTable(
                name: "MenologyRuleSchedule",
                columns: table => new
                {
                    RuleId = table.Column<int>(nullable: false),
                    ScheduleSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenologyRuleSchedule", x => new { x.ScheduleSettingsId, x.RuleId });
                });

            migrationBuilder.CreateTable(
                name: "MenologyRuleVariables",
                columns: table => new
                {
                    VariableId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    DefinitionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenologyRuleVariables", x => new { x.VariableId, x.EntityId, x.DefinitionType });
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
                    PrintDayTemplateId = table.Column<int>(nullable: true),
                    FilterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedRule_DayWorshipsFilter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "DayWorshipsFilter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Language = table.Column<string>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRuleShortNameItems", x => new { x.NameId, x.Language });
                    table.ForeignKey(
                        name: "FK_ModifiedRuleShortNameItems_ModifiedRuleShortName_NameId",
                        column: x => x.NameId,
                        principalTable: "ModifiedRuleShortName",
                        principalColumn: "ModifiedRuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriodionRulePrintLinks",
                columns: table => new
                {
                    TemplateId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriodionRulePrintLinks", x => new { x.TemplateId, x.EntityId });
                });

            migrationBuilder.CreateTable(
                name: "TriodionRuleSchedule",
                columns: table => new
                {
                    RuleId = table.Column<int>(nullable: false),
                    ScheduleSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriodionRuleSchedule", x => new { x.ScheduleSettingsId, x.RuleId });
                });

            migrationBuilder.CreateTable(
                name: "TriodionRuleVariables",
                columns: table => new
                {
                    VariableId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    DefinitionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriodionRuleVariables", x => new { x.VariableId, x.EntityId, x.DefinitionType });
                });

            migrationBuilder.CreateTable(
                name: "ExplicitAddRuleVariables",
                columns: table => new
                {
                    VariableId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplicitAddRuleVariables", x => new { x.VariableId, x.EntityId });
                });

            migrationBuilder.CreateTable(
                name: "KathismaNumberNameItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KathismaNumberNameItems", x => new { x.NameId, x.Language });
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
                });

            migrationBuilder.CreateTable(
                name: "PsalmLink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PsalmId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsalmLink_SlavaElement_SlavaElementId",
                        column: x => x.SlavaElementId,
                        principalTable: "SlavaElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputDayHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PredefinedSignId = table.Column<int>(nullable: false),
                    PrintDayTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputDayHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputDayHeader_OutputDay_Id",
                        column: x => x.Id,
                        principalTable: "OutputDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputDayHeaderNameItems",
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
                    table.PrimaryKey("PK_OutputDayHeaderNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputDayHeaderNameItems_OutputDayHeader_NameId",
                        column: x => x.NameId,
                        principalTable: "OutputDayHeader",
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
                    PrintTemplateId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sign_Sign_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Sign",
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
                name: "SignPrintLinks",
                columns: table => new
                {
                    TemplateId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignPrintLinks", x => new { x.TemplateId, x.EntityId });
                    table.ForeignKey(
                        name: "FK_SignPrintLinks_Sign_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Sign",
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
                    PrevVersionId = table.Column<int>(nullable: true),
                    VersionNumber = table.Column<int>(nullable: false),
                    PrintDayDefaultTemplateId = table.Column<int>(nullable: true),
                    IsModified = table.Column<bool>(nullable: false),
                    IsTemplate = table.Column<bool>(nullable: false),
                    CDate = table.Column<DateTime>(nullable: false),
                    BDate = table.Column<DateTime>(nullable: true),
                    EDate = table.Column<DateTime>(nullable: true),
                    ValidationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconVersion_TypiconVersion_PrevVersionId",
                        column: x => x.PrevVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TypiconVersion_TypiconEntity_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "TypiconEntity",
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
                    DaysFromEaster = table.Column<int>(nullable: true),
                    IsTransparent = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayRule_Sign_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ExplicitAddRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplicitAddRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExplicitAddRule_TypiconVersion_TypiconVersionId",
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
                    TypiconVersionId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "PrintDayTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Icon = table.Column<int>(nullable: true),
                    IsRed = table.Column<bool>(nullable: false),
                    PrintFile = table.Column<byte[]>(nullable: true),
                    PrintFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintDayTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintDayTemplate_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrintWeekTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    DaysPerPage = table.Column<int>(nullable: false),
                    PrintFile = table.Column<byte[]>(nullable: true),
                    PrintFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintWeekTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintWeekTemplate_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    IsMonday = table.Column<bool>(nullable: false),
                    IsTuesday = table.Column<bool>(nullable: false),
                    IsWednesday = table.Column<bool>(nullable: false),
                    IsThursday = table.Column<bool>(nullable: false),
                    IsFriday = table.Column<bool>(nullable: false),
                    IsSaturday = table.Column<bool>(nullable: false),
                    IsSunday = table.Column<bool>(nullable: false),
                    IncludedDates = table.Column<string>(nullable: true),
                    ExcludedDates = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleSettings_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconDescriptionItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconDescriptionItems", x => new { x.DescriptionId, x.Language });
                    table.ForeignKey(
                        name: "FK_TypiconDescriptionItems_TypiconVersion_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconNameItems",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconNameItems", x => new { x.NameId, x.Language });
                    table.ForeignKey(
                        name: "FK_TypiconNameItems_TypiconVersion_NameId",
                        column: x => x.NameId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconVariable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconVariable_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconVersionError",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConstraintDescription = table.Column<string>(nullable: true),
                    ConstraintPath = table.Column<string>(nullable: true),
                    TypiconVersionId = table.Column<int>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconVersionError", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconVersionError_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignSchedule",
                columns: table => new
                {
                    RuleId = table.Column<int>(nullable: false),
                    ScheduleSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignSchedule", x => new { x.ScheduleSettingsId, x.RuleId });
                    table.ForeignKey(
                        name: "FK_SignSchedule_Sign_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignSchedule_ScheduleSettings_ScheduleSettingsId",
                        column: x => x.ScheduleSettingsId,
                        principalTable: "ScheduleSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignVariables",
                columns: table => new
                {
                    VariableId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    DefinitionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignVariables", x => new { x.VariableId, x.EntityId, x.DefinitionType });
                    table.ForeignKey(
                        name: "FK_SignVariables_Sign_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignVariables_TypiconVariable_VariableId",
                        column: x => x.VariableId,
                        principalTable: "TypiconVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonRule_TypiconVersionId",
                table: "CommonRule",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonRuleVariables_EntityId",
                table: "CommonRuleVariables",
                column: "EntityId");

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
                name: "IX_ExplicitAddRule_TypiconVersionId",
                table: "ExplicitAddRule",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExplicitAddRuleVariables_EntityId",
                table: "ExplicitAddRuleVariables",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Kathisma_TypiconVersionId",
                table: "Kathisma",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_MenologyRulePrintLinks_EntityId",
                table: "MenologyRulePrintLinks",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MenologyRuleSchedule_RuleId",
                table: "MenologyRuleSchedule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenologyRuleVariables_EntityId",
                table: "MenologyRuleVariables",
                column: "EntityId");

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
                name: "IX_ModifiedRule_PrintDayTemplateId",
                table: "ModifiedRule",
                column: "PrintDayTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedYear_TypiconVersionId",
                table: "ModifiedYear",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputDay_TypiconId",
                table: "OutputDay",
                column: "TypiconId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputDayHeader_PredefinedSignId",
                table: "OutputDayHeader",
                column: "PredefinedSignId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputDayHeader_PrintDayTemplateId",
                table: "OutputDayHeader",
                column: "PrintDayTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputDayHeaderNameItems_NameId",
                table: "OutputDayHeaderNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputDayWorship_DayWorshipId",
                table: "OutputDayWorship",
                column: "DayWorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputWorship_OutputDayId",
                table: "OutputWorship",
                column: "OutputDayId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputWorshipAddNameItems_AddNameId",
                table: "OutputWorshipAddNameItems",
                column: "AddNameId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputWorshipNameItems_NameId",
                table: "OutputWorshipNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintDayTemplate_TypiconVersionId",
                table: "PrintDayTemplate",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintWeekTemplate_TypiconVersionId",
                table: "PrintWeekTemplate",
                column: "TypiconVersionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PsalmLink_PsalmId",
                table: "PsalmLink",
                column: "PsalmId");

            migrationBuilder.CreateIndex(
                name: "IX_PsalmLink_SlavaElementId",
                table: "PsalmLink",
                column: "SlavaElementId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSettings_TypiconVersionId",
                table: "ScheduleSettings",
                column: "TypiconVersionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sign_PrintTemplateId",
                table: "Sign",
                column: "PrintTemplateId");

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
                name: "IX_SignPrintLinks_EntityId",
                table: "SignPrintLinks",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SignSchedule_RuleId",
                table: "SignSchedule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SignVariables_EntityId",
                table: "SignVariables",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SlavaElement_KathismaId",
                table: "SlavaElement",
                column: "KathismaId");

            migrationBuilder.CreateIndex(
                name: "IX_TriodionRulePrintLinks_EntityId",
                table: "TriodionRulePrintLinks",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TriodionRuleSchedule_RuleId",
                table: "TriodionRuleSchedule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TriodionRuleVariables_EntityId",
                table: "TriodionRuleVariables",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconClaim_OwnerId",
                table: "TypiconClaim",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconClaim_TemplateId",
                table: "TypiconClaim",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconEntity_OwnerId",
                table: "TypiconEntity",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconEntity_SystemName",
                table: "TypiconEntity",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypiconEntity_TemplateId",
                table: "TypiconEntity",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVariable_TypiconVersionId",
                table: "TypiconVariable",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersion_PrevVersionId",
                table: "TypiconVersion",
                column: "PrevVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersion_PrintDayDefaultTemplateId",
                table: "TypiconVersion",
                column: "PrintDayDefaultTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersion_TypiconId",
                table: "TypiconVersion",
                column: "TypiconId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersionError_TypiconVersionId",
                table: "TypiconVersionError",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTypicon_TypiconId",
                table: "UserTypicon",
                column: "TypiconId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommonRuleVariables_CommonRule_EntityId",
                table: "CommonRuleVariables",
                column: "EntityId",
                principalTable: "CommonRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonRuleVariables_TypiconVariable_VariableId",
                table: "CommonRuleVariables",
                column: "VariableId",
                principalTable: "TypiconVariable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayRuleWorship_DayRule_DayRuleId",
                table: "DayRuleWorship",
                column: "DayRuleId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenologyRulePrintLinks_DayRule_EntityId",
                table: "MenologyRulePrintLinks",
                column: "EntityId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenologyRulePrintLinks_PrintDayTemplate_TemplateId",
                table: "MenologyRulePrintLinks",
                column: "TemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenologyRuleSchedule_DayRule_RuleId",
                table: "MenologyRuleSchedule",
                column: "RuleId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenologyRuleSchedule_ScheduleSettings_ScheduleSettingsId",
                table: "MenologyRuleSchedule",
                column: "ScheduleSettingsId",
                principalTable: "ScheduleSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenologyRuleVariables_TypiconVariable_VariableId",
                table: "MenologyRuleVariables",
                column: "VariableId",
                principalTable: "TypiconVariable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenologyRuleVariables_DayRule_EntityId",
                table: "MenologyRuleVariables",
                column: "EntityId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRule_DayRule_DayRuleId",
                table: "ModifiedRule",
                column: "DayRuleId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRule_PrintDayTemplate_PrintDayTemplateId",
                table: "ModifiedRule",
                column: "PrintDayTemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedRule_ModifiedYear_ModifiedYearId",
                table: "ModifiedRule",
                column: "ModifiedYearId",
                principalTable: "ModifiedYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriodionRulePrintLinks_DayRule_EntityId",
                table: "TriodionRulePrintLinks",
                column: "EntityId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriodionRulePrintLinks_PrintDayTemplate_TemplateId",
                table: "TriodionRulePrintLinks",
                column: "TemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriodionRuleSchedule_DayRule_RuleId",
                table: "TriodionRuleSchedule",
                column: "RuleId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriodionRuleSchedule_ScheduleSettings_ScheduleSettingsId",
                table: "TriodionRuleSchedule",
                column: "ScheduleSettingsId",
                principalTable: "ScheduleSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriodionRuleVariables_TypiconVariable_VariableId",
                table: "TriodionRuleVariables",
                column: "VariableId",
                principalTable: "TypiconVariable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriodionRuleVariables_DayRule_EntityId",
                table: "TriodionRuleVariables",
                column: "EntityId",
                principalTable: "DayRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExplicitAddRuleVariables_TypiconVariable_VariableId",
                table: "ExplicitAddRuleVariables",
                column: "VariableId",
                principalTable: "TypiconVariable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExplicitAddRuleVariables_ExplicitAddRule_EntityId",
                table: "ExplicitAddRuleVariables",
                column: "EntityId",
                principalTable: "ExplicitAddRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KathismaNumberNameItems_Kathisma_NameId",
                table: "KathismaNumberNameItems",
                column: "NameId",
                principalTable: "Kathisma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlavaElement_Kathisma_KathismaId",
                table: "SlavaElement",
                column: "KathismaId",
                principalTable: "Kathisma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutputDayHeader_Sign_PredefinedSignId",
                table: "OutputDayHeader",
                column: "PredefinedSignId",
                principalTable: "Sign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutputDayHeader_PrintDayTemplate_PrintDayTemplateId",
                table: "OutputDayHeader",
                column: "PrintDayTemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sign_TypiconVersion_TypiconVersionId",
                table: "Sign",
                column: "TypiconVersionId",
                principalTable: "TypiconVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sign_PrintDayTemplate_PrintTemplateId",
                table: "Sign",
                column: "PrintTemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignPrintLinks_PrintDayTemplate_TemplateId",
                table: "SignPrintLinks",
                column: "TemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypiconVersion_PrintDayTemplate_PrintDayDefaultTemplateId",
                table: "TypiconVersion",
                column: "PrintDayDefaultTemplateId",
                principalTable: "PrintDayTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintDayTemplate_TypiconVersion_TypiconVersionId",
                table: "PrintDayTemplate");

            migrationBuilder.DropTable(
                name: "CommonRuleVariables");

            migrationBuilder.DropTable(
                name: "DayRuleWorship");

            migrationBuilder.DropTable(
                name: "DayWorshipNameItems");

            migrationBuilder.DropTable(
                name: "DayWorshipShortNameItems");

            migrationBuilder.DropTable(
                name: "EasterItem");

            migrationBuilder.DropTable(
                name: "ExplicitAddRuleVariables");

            migrationBuilder.DropTable(
                name: "Katavasia");

            migrationBuilder.DropTable(
                name: "KathismaNumberNameItems");

            migrationBuilder.DropTable(
                name: "MenologyRulePrintLinks");

            migrationBuilder.DropTable(
                name: "MenologyRuleSchedule");

            migrationBuilder.DropTable(
                name: "MenologyRuleVariables");

            migrationBuilder.DropTable(
                name: "ModifiedRuleShortNameItems");

            migrationBuilder.DropTable(
                name: "OktoikhDay");

            migrationBuilder.DropTable(
                name: "OutputDayHeaderNameItems");

            migrationBuilder.DropTable(
                name: "OutputDayWorship");

            migrationBuilder.DropTable(
                name: "OutputWorshipAddNameItems");

            migrationBuilder.DropTable(
                name: "OutputWorshipNameItems");

            migrationBuilder.DropTable(
                name: "PrintWeekTemplate");

            migrationBuilder.DropTable(
                name: "PsalmLink");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SignNameItems");

            migrationBuilder.DropTable(
                name: "SignPrintLinks");

            migrationBuilder.DropTable(
                name: "SignSchedule");

            migrationBuilder.DropTable(
                name: "SignVariables");

            migrationBuilder.DropTable(
                name: "TheotokionApp");

            migrationBuilder.DropTable(
                name: "TriodionRulePrintLinks");

            migrationBuilder.DropTable(
                name: "TriodionRuleSchedule");

            migrationBuilder.DropTable(
                name: "TriodionRuleVariables");

            migrationBuilder.DropTable(
                name: "TypiconClaimDescriptionItems");

            migrationBuilder.DropTable(
                name: "TypiconClaimNameItems");

            migrationBuilder.DropTable(
                name: "TypiconDescriptionItems");

            migrationBuilder.DropTable(
                name: "TypiconNameItems");

            migrationBuilder.DropTable(
                name: "TypiconVersionError");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UserTypicon");

            migrationBuilder.DropTable(
                name: "WeekDayApp");

            migrationBuilder.DropTable(
                name: "CommonRule");

            migrationBuilder.DropTable(
                name: "DayWorshipShortName");

            migrationBuilder.DropTable(
                name: "ExplicitAddRule");

            migrationBuilder.DropTable(
                name: "ModifiedRuleShortName");

            migrationBuilder.DropTable(
                name: "OutputDayHeader");

            migrationBuilder.DropTable(
                name: "OutputWorshipAddName");

            migrationBuilder.DropTable(
                name: "Psalm");

            migrationBuilder.DropTable(
                name: "SlavaElement");

            migrationBuilder.DropTable(
                name: "ScheduleSettings");

            migrationBuilder.DropTable(
                name: "TypiconVariable");

            migrationBuilder.DropTable(
                name: "TypiconClaim");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DayWorship");

            migrationBuilder.DropTable(
                name: "ModifiedRule");

            migrationBuilder.DropTable(
                name: "OutputWorship");

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
                name: "OutputDay");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "TypiconVersion");

            migrationBuilder.DropTable(
                name: "PrintDayTemplate");

            migrationBuilder.DropTable(
                name: "TypiconEntity");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
