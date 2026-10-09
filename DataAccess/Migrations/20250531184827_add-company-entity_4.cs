using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addcompanyentity_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_CompanyId",
                table: "IcsrReports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_CreatedByUserId",
                table: "IcsrReports",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_ModifiedByUserId",
                table: "IcsrReports",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IcsrReports_Company_CompanyId",
                table: "IcsrReports",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_IcsrReports_Users_CreatedByUserId",
                table: "IcsrReports",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IcsrReports_Users_ModifiedByUserId",
                table: "IcsrReports",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Company_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IcsrReports_Company_CompanyId",
                table: "IcsrReports");

            migrationBuilder.DropForeignKey(
                name: "FK_IcsrReports_Users_CreatedByUserId",
                table: "IcsrReports");

            migrationBuilder.DropForeignKey(
                name: "FK_IcsrReports_Users_ModifiedByUserId",
                table: "IcsrReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Company_CompanyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_CompanyId",
                table: "IcsrReports");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_CreatedByUserId",
                table: "IcsrReports");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_ModifiedByUserId",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "IcsrReports");
        }
    }
}
