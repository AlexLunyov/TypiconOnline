using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class MigrationVersionedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Typicon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typicon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypiconId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sign_Typicon_TypiconId",
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
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    ArchiveDate = table.Column<DateTime>(nullable: true),
                    PreviousVersionId = table.Column<int>(nullable: true),
                    TypiconId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypiconVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypiconVersion_TypiconVersion_PreviousVersionId",
                        column: x => x.PreviousVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypiconVersion_Typicon_TypiconId",
                        column: x => x.TypiconId,
                        principalTable: "Typicon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    ArchiveDate = table.Column<DateTime>(nullable: true),
                    PreviousVersionId = table.Column<int>(nullable: true),
                    VersionOwnerId = table.Column<int>(nullable: false),
                    RuleDefinition = table.Column<string>(nullable: true),
                    ModRuleDefinition = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false),
                    TypiconVersionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignVersion_SignVersion_PreviousVersionId",
                        column: x => x.PreviousVersionId,
                        principalTable: "SignVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignVersion_Sign_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignVersion_TypiconVersion_TypiconVersionId",
                        column: x => x.TypiconVersionId,
                        principalTable: "TypiconVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignVersion_Sign_VersionOwnerId",
                        column: x => x.VersionOwnerId,
                        principalTable: "Sign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignNameItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignNameItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignNameItems_SignVersion_NameId",
                        column: x => x.NameId,
                        principalTable: "SignVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sign_TypiconId",
                table: "Sign",
                column: "TypiconId");

            migrationBuilder.CreateIndex(
                name: "IX_SignNameItems_NameId",
                table: "SignNameItems",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_SignVersion_PreviousVersionId",
                table: "SignVersion",
                column: "PreviousVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_SignVersion_TemplateId",
                table: "SignVersion",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SignVersion_TypiconVersionId",
                table: "SignVersion",
                column: "TypiconVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_SignVersion_VersionOwnerId",
                table: "SignVersion",
                column: "VersionOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersion_PreviousVersionId",
                table: "TypiconVersion",
                column: "PreviousVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_TypiconVersion_TypiconId",
                table: "TypiconVersion",
                column: "TypiconId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignNameItems");

            migrationBuilder.DropTable(
                name: "SignVersion");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "TypiconVersion");

            migrationBuilder.DropTable(
                name: "Typicon");
        }
    }
}
