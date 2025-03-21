using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramInformationV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "FieldSources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SecurityEntries",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 28, 16, 25, 28, 290, DateTimeKind.Local).AddTicks(8199));

            migrationBuilder.UpdateData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "LastUpdated", "UseCourses", "UseCredentials", "UsePrograms", "UseRequirementSets", "UseSections" },
                values: new object[] { new DateTime(2025, 2, 28, 16, 25, 28, 290, DateTimeKind.Local).AddTicks(8087), true, true, true, true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "FieldSources");

            migrationBuilder.UpdateData(
                table: "SecurityEntries",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 28, 15, 28, 33, 841, DateTimeKind.Local).AddTicks(6458));

            migrationBuilder.UpdateData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "LastUpdated", "UseCourses", "UseCredentials", "UsePrograms", "UseRequirementSets", "UseSections" },
                values: new object[] { new DateTime(2025, 2, 28, 15, 28, 33, 841, DateTimeKind.Local).AddTicks(6349), false, false, false, false, false });
        }
    }
}
