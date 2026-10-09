using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_indexes_for_SafetyReportId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports");

            migrationBuilder.AlterColumn<string>(
                name: "SafetyReportId",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SafetyReportId",
                table: "IcsrReports",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports",
                column: "SafetyReportId",
                unique: true,
                filter: "[SafetyReportId] IS NOT NULL");
        }
    }
}
