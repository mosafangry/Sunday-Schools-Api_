using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class int07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Summaries_PatientId",
                table: "Summaries");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_MessageHeaderId",
                table: "IcsrReports");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstances_DrugId",
                table: "ActiveSubstances");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Summaries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NarrativeIncludeClinical",
                table: "Summaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderTitle",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderTel",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderStreetAddress",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderOrganization",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderGiveName",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderEmailAddress",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderDepartment",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SenderCountryCode",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverOrganization",
                table: "Receivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCountryCode",
                table: "Receivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraVersionPT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraVersionLLT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraPT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraLLT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PrimarySourceReaction",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Reactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReactionDurationUnit",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReactionEndDate",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReactionEndDateFormat",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReactionStartDate",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReactionStartDateFormat",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReporterFamilyName",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ReporterCountry",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterOrganization",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterPostCode",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterState",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientInitial",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientBirthDate",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GestationPeriod",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GestationPeriodUnit",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastMenstrualDateFormat",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientGpMedicalRecordNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientHospitalRecordNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientInvestigationNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientLastMenstrualDate",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PatientOnsetAge",
                table: "Patients",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientOnsetAgeUnit",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientSpecialistRecordNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageType",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageSenderIdentifier",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageReceiverIdentifier",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageNumber",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageFormatVersion",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageFormatRelease",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MessageDateFormat",
                table: "MessageHeaders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MessageDate",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TransmissionDateFormat",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TransmissionDate",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Serious",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SafetyReportVersion",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SafetyReportId",
                table: "IcsrReports",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ReportType",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDateFormat",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptDateFormat",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptDate",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OccurCountry",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MessageHeaderId",
                table: "IcsrReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNumber",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PrimarySourceCountry",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiveDate",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriousnessCongenitalAnomaly",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriousnessDeath",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriousnessDisabling",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriousnessHospitalization",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriousnessLifeThreatening",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriousnessOther",
                table: "IcsrReports",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Drugs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ObtainDrugCountry",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MedicinalProduct",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DrugIndicationMeddraVersion",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DrugAdministrationRoute",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ActiveSubstanceName",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugAuthorizationCountry",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugEndDate",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrugEndDateFormat",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugIndication",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrugRecurReadministration",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugStartDate",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrugStartDateFormat",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugReactionAssesMeddraVersion",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DrugReactionAsses",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DrugId",
                table: "DrugReactionRelatedness",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DrugAssessmentSource",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DrugAssessmentMethod",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrugResult",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicationName",
                table: "ConcomitantMedications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IcsrId",
                table: "ConcomitantMedications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IcsrId",
                table: "ConcomitantDiseases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseName",
                table: "ConcomitantDiseases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DrugId",
                table: "ActiveSubstances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveSubstanceName",
                table: "ActiveSubstances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_PatientId",
                table: "Summaries",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_MessageHeaderId",
                table: "IcsrReports",
                column: "MessageHeaderId",
                unique: true,
                filter: "[MessageHeaderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports",
                column: "SafetyReportId",
                unique: true,
                filter: "[SafetyReportId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstances_DrugId",
                table: "ActiveSubstances",
                column: "DrugId",
                unique: true,
                filter: "[DrugId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Summaries_PatientId",
                table: "Summaries");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_MessageHeaderId",
                table: "IcsrReports");

            migrationBuilder.DropIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstances_DrugId",
                table: "ActiveSubstances");

            migrationBuilder.DropColumn(
                name: "SenderCountryCode",
                table: "Senders");

            migrationBuilder.DropColumn(
                name: "ReceiverCountryCode",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "ReactionDurationUnit",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReactionEndDate",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReactionEndDateFormat",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReactionStartDate",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReactionStartDateFormat",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReporterCountry",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "ReporterOrganization",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "ReporterPostCode",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "ReporterState",
                table: "PrimarySources");

            migrationBuilder.DropColumn(
                name: "GestationPeriod",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "GestationPeriodUnit",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastMenstrualDateFormat",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientGpMedicalRecordNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientHospitalRecordNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientInvestigationNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientLastMenstrualDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientOnsetAge",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientOnsetAgeUnit",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientSpecialistRecordNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PrimarySourceCountry",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "SeriousnessCongenitalAnomaly",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "SeriousnessDeath",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "SeriousnessDisabling",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "SeriousnessHospitalization",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "SeriousnessLifeThreatening",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "SeriousnessOther",
                table: "IcsrReports");

            migrationBuilder.DropColumn(
                name: "ActiveSubstanceName",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugAuthorizationCountry",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugEndDate",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugEndDateFormat",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugIndication",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugRecurReadministration",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugStartDate",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugStartDateFormat",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "DrugAssessmentMethod",
                table: "DrugReactionRelatedness");

            migrationBuilder.DropColumn(
                name: "DrugResult",
                table: "DrugReactionRelatedness");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Summaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NarrativeIncludeClinical",
                table: "Summaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderTitle",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderTel",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderStreetAddress",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderOrganization",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderGiveName",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderEmailAddress",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderDepartment",
                table: "Senders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverOrganization",
                table: "Receivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraVersionPT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraVersionLLT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraPT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReactionMeddraLLT",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimarySourceReaction",
                table: "Reactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Reactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReporterFamilyName",
                table: "PrimarySources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientInitial",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientBirthDate",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageType",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageSenderIdentifier",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageReceiverIdentifier",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageNumber",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageFormatVersion",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageFormatRelease",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MessageDateFormat",
                table: "MessageHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageDate",
                table: "MessageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TransmissionDateFormat",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransmissionDate",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Serious",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SafetyReportVersion",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SafetyReportId",
                table: "IcsrReports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportType",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDateFormat",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptDateFormat",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptDate",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OccurCountry",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MessageHeaderId",
                table: "IcsrReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNumber",
                table: "IcsrReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Drugs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ObtainDrugCountry",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicinalProduct",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugIndicationMeddraVersion",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugAdministrationRoute",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugReactionAssesMeddraVersion",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugReactionAsses",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrugId",
                table: "DrugReactionRelatedness",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugAssessmentSource",
                table: "DrugReactionRelatedness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicationName",
                table: "ConcomitantMedications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IcsrId",
                table: "ConcomitantMedications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IcsrId",
                table: "ConcomitantDiseases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseName",
                table: "ConcomitantDiseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrugId",
                table: "ActiveSubstances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActiveSubstanceName",
                table: "ActiveSubstances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_PatientId",
                table: "Summaries",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_MessageHeaderId",
                table: "IcsrReports",
                column: "MessageHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports",
                column: "SafetyReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstances_DrugId",
                table: "ActiveSubstances",
                column: "DrugId",
                unique: true);
        }
    }
}
