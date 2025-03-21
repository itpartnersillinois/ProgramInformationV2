using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramInformationV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryType = table.Column<int>(type: "int", nullable: false),
                    ChangedByNetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSent = table.Column<bool>(type: "bit", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTest = table.Column<bool>(type: "bit", nullable: false),
                    RequestDeletion = table.Column<bool>(type: "bit", nullable: false),
                    RequestDeletionByEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseCourses = table.Column<bool>(type: "bit", nullable: false),
                    UseCredentials = table.Column<bool>(type: "bit", nullable: false),
                    UsePrograms = table.Column<bool>(type: "bit", nullable: false),
                    UseRequirementSets = table.Column<bool>(type: "bit", nullable: false),
                    UseSections = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowItem = table.Column<bool>(type: "bit", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldSources_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFullAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsRequested = table.Column<bool>(type: "bit", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityEntries_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    TagType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagSources_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Code", "CreatedByEmail", "IsActive", "IsTest", "LastUpdated", "RequestDeletion", "RequestDeletionByEmail", "Title", "UseCourses", "UseCredentials", "UsePrograms", "UseRequirementSets", "UseSections" },
                values: new object[] { -1, "test", "jonker@illinois.edu", false, true, new DateTime(2025, 2, 28, 15, 28, 33, 841, DateTimeKind.Local).AddTicks(6349), false, "", "Test Entry", false, false, false, false, false });

            migrationBuilder.InsertData(
                table: "SecurityEntries",
                columns: new[] { "Id", "DepartmentTag", "Email", "IsActive", "IsFullAdmin", "IsOwner", "IsPublic", "IsRequested", "LastUpdated", "SourceId" },
                values: new object[] { -1, "", "jonker@illinois.edu", true, false, true, false, false, new DateTime(2025, 2, 28, 15, 28, 33, 841, DateTimeKind.Local).AddTicks(6458), -1 });

            migrationBuilder.CreateIndex(
                name: "IX_FieldSources_SourceId",
                table: "FieldSources",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityEntries_SourceId",
                table: "SecurityEntries",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TagSources_SourceId",
                table: "TagSources",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldSources");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "SecurityEntries");

            migrationBuilder.DropTable(
                name: "TagSources");

            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
