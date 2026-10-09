using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class make_drug_patient_many_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_Patients_PatientId",
                table: "Drugs");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_PatientId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Drugs");

            migrationBuilder.CreateTable(
                name: "DrugEntityPatientEntity",
                columns: table => new
                {
                    DrugsId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugEntityPatientEntity", x => new { x.DrugsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_DrugEntityPatientEntity_Drugs_DrugsId",
                        column: x => x.DrugsId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugEntityPatientEntity_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugEntityPatientEntity_PatientsId",
                table: "DrugEntityPatientEntity",
                column: "PatientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugEntityPatientEntity");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_PatientId",
                table: "Drugs",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Patients_PatientId",
                table: "Drugs",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
