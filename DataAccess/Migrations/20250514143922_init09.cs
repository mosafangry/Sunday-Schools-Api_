using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Summaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Summaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Summaries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Summaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Summaries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Senders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Senders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Senders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Receivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Receivers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Receivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Receivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Receivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Reactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Reactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PrimarySources",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PrimarySources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "PrimarySources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MessageHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MessageHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MessageHeaders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "IcsrReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IcsrReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "IcsrReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Drugs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Drugs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Drugs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DrugReactionRelatedness",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DrugReactionRelatedness",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "DrugReactionRelatedness",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ConcomitantMedications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ConcomitantMedications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ConcomitantMedications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ConcomitantMedications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ConcomitantMedications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ConcomitantDiseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ConcomitantDiseases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ConcomitantDiseases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ConcomitantDiseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ConcomitantDiseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ActiveSubstances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ActiveSubstances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ActiveSubstances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ActiveSubstances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ActiveSubstances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FDAValidationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SafetyReportId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDAValidationResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FDAValidationErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalXmlValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FDAValidationResultId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDAValidationErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FDAValidationErrors_FDAValidationResults_FDAValidationResultId",
                        column: x => x.FDAValidationResultId,
                        principalTable: "FDAValidationResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FDAValidationErrors_FDAValidationResultId",
                table: "FDAValidationErrors",
                column: "FDAValidationResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FDAValidationErrors");

            migrationBuilder.DropTable(
                name: "FDAValidationResults");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MessageHeaders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MessageHeaders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MessageHeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "MessageHeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MessageHeaders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DrugReactionRelatedness");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DrugReactionRelatedness");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DrugReactionRelatedness");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DrugReactionRelatedness");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "DrugReactionRelatedness");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ConcomitantMedications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ConcomitantMedications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ConcomitantMedications");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ConcomitantMedications");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ConcomitantMedications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ConcomitantDiseases");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ConcomitantDiseases");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ConcomitantDiseases");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ConcomitantDiseases");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ConcomitantDiseases");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ActiveSubstances");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ActiveSubstances");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ActiveSubstances");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ActiveSubstances");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ActiveSubstances");
        }
    }
}
