using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TypiconMigrationTool.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasterItem",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasterItem", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "ItemDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Expression = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Katavasia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katavasia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OktoikhDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    Ihos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OktoikhDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Psalm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psalm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheotokionApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    Ihos = table.Column<int>(type: "INTEGER", nullable: false),
                    Place = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheotokionApp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypiconEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconEntity_TypiconEntity_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    DateBId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateId = table.Column<int>(type: "INTEGER", nullable: true),
                    DaysFromEaster = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Day_ItemDate_DateBId",
                        column: x => x.DateBId,
                        principalTable: "ItemDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Day_ItemDate_DateId",
                        column: x => x.DateId,
                        principalTable: "ItemDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommonRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    TypiconEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonRule_TypiconEntity_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommonRule_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kathisma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    TypiconEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberName_StringExpression = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kathisma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kathisma_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModifiedYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypiconEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedYear_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypiconSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefaultLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    TypiconEntityId = table.Column<int>(name: "TypiconEntity.Id", type: "INTEGER", nullable: true),
                    TypiconEntityId0 = table.Column<int>(name: "TypiconEntityId", type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconSettings_TypiconEntity_TypiconEntity.Id",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypiconSettings_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId0,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlavaElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KathismaId = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "Sign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAddition = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTemplate = table.Column<bool>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(name: "Owner.Id", type: "INTEGER", nullable: true),
                    OwnerId0 = table.Column<int>(name: "OwnerId", type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    TypiconEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    SignName_StringExpression = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sign_TypiconSettings_Owner.Id",
                        column: x => x.OwnerId,
                        principalTable: "TypiconSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sign_TypiconEntity_OwnerId",
                        column: x => x.OwnerId0,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sign_Sign_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sign_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PsalmLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EndStihos = table.Column<int>(type: "INTEGER", nullable: true),
                    PsalmId = table.Column<int>(type: "INTEGER", nullable: true),
                    SlavaElementId = table.Column<int>(type: "INTEGER", nullable: true),
                    StartStihos = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "DayRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    IsAddition = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateBId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateId = table.Column<int>(type: "INTEGER", nullable: true),
                    DaysFromEaster = table.Column<int>(type: "INTEGER", nullable: true)
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
                        name: "FK_DayRule_ItemDate_DateBId",
                        column: x => x.DateBId,
                        principalTable: "ItemDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRule_ItemDate_DateId",
                        column: x => x.DateId,
                        principalTable: "ItemDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRule_TypiconEntity_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayWorship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayRuleId = table.Column<int>(type: "INTEGER", nullable: true),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    IsCelebrating = table.Column<bool>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    UseFullName = table.Column<bool>(type: "INTEGER", nullable: false),
                    WorshipName_StringExpression = table.Column<string>(type: "TEXT", nullable: true),
                    WorshipShortName_StringExpression = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayWorship_DayRule_DayRuleId",
                        column: x => x.DayRuleId,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayWorship_Day_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifiedRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsAddition = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLastName = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifiedYearId = table.Column<int>(type: "INTEGER", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    UseFullName = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifiedRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifiedRule_ModifiedYear_ModifiedYearId",
                        column: x => x.ModifiedYearId,
                        principalTable: "ModifiedYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModifiedRule_DayRule_RuleEntityId",
                        column: x => x.RuleEntityId,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayRuleWorship",
                columns: table => new
                {
                    DayRuleId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayWorshipId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayRuleId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
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
                        name: "FK_DayRuleWorship_DayRule_DayRuleId1",
                        column: x => x.DayRuleId1,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRuleWorship_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonRule_OwnerId",
                table: "CommonRule",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonRule_TypiconEntityId",
                table: "CommonRule",
                column: "TypiconEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Day_DateBId",
                table: "Day",
                column: "DateBId");

            migrationBuilder.CreateIndex(
                name: "IX_Day_DateId",
                table: "Day",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_TemplateId",
                table: "DayRule",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_DateBId",
                table: "DayRule",
                column: "DateBId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_DateId",
                table: "DayRule",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_OwnerId",
                table: "DayRule",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorship_DayRuleId1",
                table: "DayRuleWorship",
                column: "DayRuleId1");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorship_DayWorshipId",
                table: "DayRuleWorship",
                column: "DayWorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorship_DayRuleId",
                table: "DayWorship",
                column: "DayRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorship_ParentId",
                table: "DayWorship",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Kathisma_TypiconEntityId",
                table: "Kathisma",
                column: "TypiconEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRule_ModifiedYearId",
                table: "ModifiedRule",
                column: "ModifiedYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedRule_RuleEntityId",
                table: "ModifiedRule",
                column: "RuleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedYear_TypiconEntityId",
                table: "ModifiedYear",
                column: "TypiconEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PsalmLink_PsalmId",
                table: "PsalmLink",
                column: "PsalmId");

            migrationBuilder.CreateIndex(
                name: "IX_PsalmLink_SlavaElementId",
                table: "PsalmLink",
                column: "SlavaElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_Owner.Id",
                table: "Sign",
                column: "Owner.Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sign_OwnerId",
                table: "Sign",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TemplateId",
                table: "Sign",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TypiconEntityId",
                table: "Sign",
                column: "TypiconEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SlavaElement_KathismaId",
                table: "SlavaElement",
                column: "KathismaId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconEntity_TemplateId",
                table: "TypiconEntity",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconSettings_TypiconEntity.Id",
                table: "TypiconSettings",
                column: "TypiconEntity.Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypiconSettings_TypiconEntityId",
                table: "TypiconSettings",
                column: "TypiconEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonRule");

            migrationBuilder.DropTable(
                name: "DayRuleWorship");

            migrationBuilder.DropTable(
                name: "EasterItem");

            migrationBuilder.DropTable(
                name: "Katavasia");

            migrationBuilder.DropTable(
                name: "ModifiedRule");

            migrationBuilder.DropTable(
                name: "OktoikhDay");

            migrationBuilder.DropTable(
                name: "PsalmLink");

            migrationBuilder.DropTable(
                name: "TheotokionApp");

            migrationBuilder.DropTable(
                name: "DayWorship");

            migrationBuilder.DropTable(
                name: "ModifiedYear");

            migrationBuilder.DropTable(
                name: "Psalm");

            migrationBuilder.DropTable(
                name: "SlavaElement");

            migrationBuilder.DropTable(
                name: "DayRule");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "Kathisma");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "ItemDate");

            migrationBuilder.DropTable(
                name: "TypiconSettings");

            migrationBuilder.DropTable(
                name: "TypiconEntity");
        }
    }
}
