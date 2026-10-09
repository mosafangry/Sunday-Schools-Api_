using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class relation_bwtween_drugs_and_comapny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_CompanyId",
                table: "Drugs",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Companies_CompanyId",
                table: "Drugs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_Companies_CompanyId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_CompanyId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Drugs");
        }
    }
}
