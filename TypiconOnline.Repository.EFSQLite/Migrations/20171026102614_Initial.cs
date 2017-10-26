using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TypiconOnline.Repository.EFSQLite.Migrations
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
                name: "ItemText",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StringExpression = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemText", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OktoikhDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    Ihos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OktoikhDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheotokionApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    Ihos = table.Column<int>(type: "INTEGER", nullable: false),
                    Place = table.Column<int>(type: "INTEGER", nullable: false),
                    StringDefinition = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "ModifiedYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypiconEntityId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypiconSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefaultLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    IsExceptionThrownWhenInvalid = table.Column<bool>(type: "INTEGER", nullable: false),
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
                name: "Sign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAddition = table.Column<bool>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(name: "Owner.Id", type: "INTEGER", nullable: true),
                    OwnerId0 = table.Column<int>(name: "OwnerId", type: "INTEGER", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    SignNameId = table.Column<int>(type: "INTEGER", nullable: true),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    TypiconEntityId = table.Column<int>(type: "INTEGER", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sign_ItemText_SignNameId",
                        column: x => x.SignNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "DayRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    IsAddition = table.Column<bool>(type: "INTEGER", nullable: false),
                    RuleDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateBId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateId = table.Column<int>(type: "INTEGER", nullable: true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: true),
                    DaysFromEaster = table.Column<int>(type: "INTEGER", nullable: true),
                    TriodionRule_OwnerId = table.Column<int>(type: "INTEGER", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRule_TypiconEntity_TriodionRule_OwnerId",
                        column: x => x.TriodionRule_OwnerId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayWorship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayDefinition = table.Column<string>(type: "TEXT", nullable: true),
                    DayRuleId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsCelebrating = table.Column<bool>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    UseFullName = table.Column<bool>(type: "INTEGER", nullable: false),
                    WorshipNameId = table.Column<int>(type: "INTEGER", nullable: true),
                    WorshipShortNameId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_DayWorship_ItemText_WorshipNameId",
                        column: x => x.WorshipNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayWorship_ItemText_WorshipShortNameId",
                        column: x => x.WorshipShortNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "DayRuleWorships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayRuleId = table.Column<int>(type: "INTEGER", nullable: true),
                    DayRuleId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    DayWorshipId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRuleWorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayRuleWorships_DayRule_DayRuleId",
                        column: x => x.DayRuleId,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRuleWorships_DayRule_DayRuleId1",
                        column: x => x.DayRuleId1,
                        principalTable: "DayRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayRuleWorships_DayWorship_DayWorshipId",
                        column: x => x.DayWorshipId,
                        principalTable: "DayWorship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_DayRule_TriodionRule_OwnerId",
                table: "DayRule",
                column: "TriodionRule_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorships_DayRuleId",
                table: "DayRuleWorships",
                column: "DayRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorships_DayRuleId1",
                table: "DayRuleWorships",
                column: "DayRuleId1");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorships_DayWorshipId",
                table: "DayRuleWorships",
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
                name: "IX_DayWorship_WorshipNameId",
                table: "DayWorship",
                column: "WorshipNameId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorship_WorshipShortNameId",
                table: "DayWorship",
                column: "WorshipShortNameId");

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
                name: "IX_Sign_Owner.Id",
                table: "Sign",
                column: "Owner.Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sign_OwnerId",
                table: "Sign",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_SignNameId",
                table: "Sign",
                column: "SignNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TemplateId",
                table: "Sign",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TypiconEntityId",
                table: "Sign",
                column: "TypiconEntityId");

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
                name: "DayRuleWorships");

            migrationBuilder.DropTable(
                name: "EasterItem");

            migrationBuilder.DropTable(
                name: "ModifiedRule");

            migrationBuilder.DropTable(
                name: "OktoikhDay");

            migrationBuilder.DropTable(
                name: "TheotokionApp");

            migrationBuilder.DropTable(
                name: "DayWorship");

            migrationBuilder.DropTable(
                name: "ModifiedYear");

            migrationBuilder.DropTable(
                name: "DayRule");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "ItemDate");

            migrationBuilder.DropTable(
                name: "TypiconSettings");

            migrationBuilder.DropTable(
                name: "ItemText");

            migrationBuilder.DropTable(
                name: "TypiconEntity");
        }
    }
}
