using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TypiconMigrationTool.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayWorshipsFilter",
                columns: table => new
                {
                    ExcludedItem = table.Column<int>(nullable: true),
                    IncludedItem = table.Column<int>(nullable: true),
                    IsCelebrating = table.Column<bool>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
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
                name: "ItemDate",
                columns: table => new
                {
                    Expression = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemText",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    IsBold = table.Column<bool>(nullable: true),
                    IsItalic = table.Column<bool>(nullable: true),
                    IsRed = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemText", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Katavasia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "TypiconEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    DefaultLanguage = table.Column<string>(nullable: true)
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
                name: "WeekDayApp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Definition = table.Column<string>(nullable: true),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDayApp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    DateId = table.Column<int>(nullable: true),
                    DateBId = table.Column<int>(nullable: true),
                    DaysFromEaster = table.Column<int>(nullable: true)
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
                name: "ItemTextUnit",
                columns: table => new
                {
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemTextId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTextUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTextUnit_ItemText_ItemTextId",
                        column: x => x.ItemTextId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommonRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TypiconEntityId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonRule_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kathisma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TypiconEntityId = table.Column<int>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    NumberNameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kathisma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kathisma_ItemText_NumberNameId",
                        column: x => x.NumberNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TypiconEntityId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
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
                name: "Sign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TypiconEntityId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    IsAddition = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    IsTemplate = table.Column<bool>(nullable: false),
                    SignNameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sign", x => x.Id);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayWorship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Definition = table.Column<string>(nullable: true),
                    WorshipNameId = table.Column<int>(nullable: false),
                    WorshipShortNameId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_DayWorship_ItemText_WorshipNameId",
                        column: x => x.WorshipNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayWorship_ItemText_WorshipShortNameId",
                        column: x => x.WorshipShortNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlavaElement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TypiconEntityId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DateId = table.Column<int>(nullable: true),
                    DateBId = table.Column<int>(nullable: true),
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
                        name: "FK_DayRule_TypiconEntity_TypiconEntityId",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayRule_TypiconEntity_TypiconEntityId1",
                        column: x => x.TypiconEntityId,
                        principalTable: "TypiconEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsalmLink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ModifiedYearId = table.Column<int>(nullable: false),
                    DayRuleId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    IsLastName = table.Column<bool>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false),
                    UseFullName = table.Column<bool>(nullable: false),
                    SignNumber = table.Column<int>(nullable: true),
                    ShortNameId = table.Column<int>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ModifiedRule_ItemText_ShortNameId",
                        column: x => x.ShortNameId,
                        principalTable: "ItemText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_DayRule_TypiconEntityId",
                table: "DayRule",
                column: "TypiconEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRule_TypiconEntityId1",
                table: "DayRule",
                column: "TypiconEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRuleWorship_DayWorshipId",
                table: "DayRuleWorship",
                column: "DayWorshipId");

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
                name: "IX_ItemTextUnit_ItemTextId",
                table: "ItemTextUnit",
                column: "ItemTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Kathisma_NumberNameId",
                table: "Kathisma",
                column: "NumberNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Kathisma_TypiconEntityId",
                table: "Kathisma",
                column: "TypiconEntityId");

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
                name: "IX_ModifiedRule_ShortNameId",
                table: "ModifiedRule",
                column: "ShortNameId");

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
                name: "IX_SlavaElement_KathismaId",
                table: "SlavaElement",
                column: "KathismaId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconEntity_TemplateId",
                table: "TypiconEntity",
                column: "TemplateId");
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
                name: "ItemTextUnit");

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
                name: "WeekDayApp");

            migrationBuilder.DropTable(
                name: "DayWorship");

            migrationBuilder.DropTable(
                name: "DayRule");

            migrationBuilder.DropTable(
                name: "DayWorshipsFilter");

            migrationBuilder.DropTable(
                name: "ModifiedYear");

            migrationBuilder.DropTable(
                name: "Psalm");

            migrationBuilder.DropTable(
                name: "SlavaElement");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "Kathisma");

            migrationBuilder.DropTable(
                name: "ItemDate");

            migrationBuilder.DropTable(
                name: "ItemText");

            migrationBuilder.DropTable(
                name: "TypiconEntity");
        }
    }
}
