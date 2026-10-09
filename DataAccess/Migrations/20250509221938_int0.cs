using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class int0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageFormatVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageFormatRelease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageSenderIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageReceiverIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageDateFormat = table.Column<int>(type: "int", nullable: false),
                    MessageDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientInitial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientBirthDateFormat = table.Column<int>(type: "int", nullable: true),
                    PatientBirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAgeGroup = table.Column<int>(type: "int", nullable: true),
                    PatientWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PatientHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PatientSex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimarySources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReporterFamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimarySources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverType = table.Column<int>(type: "int", nullable: true),
                    ReceiverOrganization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderType = table.Column<int>(type: "int", nullable: true),
                    SenderOrganization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderGiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderStreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugCharacterization = table.Column<int>(type: "int", nullable: true),
                    MedicinalProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObtainDrugCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugAdministrationRoute = table.Column<int>(type: "int", nullable: true),
                    DrugIndicationMeddraVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionDrug = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drugs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimarySourceReaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactionMeddraVersionLLT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactionMeddraLLT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactionMeddraVersionPT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactionMeddraPT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermHighlighted = table.Column<int>(type: "int", nullable: true),
                    ReactionOutcome = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Summaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NarrativeIncludeClinical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Summaries_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IcsrReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SafetyReportVersion = table.Column<int>(type: "int", nullable: false),
                    OccurCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransmissionDateFormat = table.Column<int>(type: "int", nullable: false),
                    TransmissionDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    Serious = table.Column<int>(type: "int", nullable: false),
                    ReceiveDateFormat = table.Column<int>(type: "int", nullable: false),
                    ReceiptDateFormat = table.Column<int>(type: "int", nullable: false),
                    ReceiptDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalDocument = table.Column<int>(type: "int", nullable: true),
                    FulfillExpediteCriteria = table.Column<int>(type: "int", nullable: true),
                    CompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageHeaderId = table.Column<int>(type: "int", nullable: false),
                    PrimarySourceId = table.Column<int>(type: "int", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcsrReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IcsrReports_MessageHeaders_MessageHeaderId",
                        column: x => x.MessageHeaderId,
                        principalTable: "MessageHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IcsrReports_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IcsrReports_PrimarySources_PrimarySourceId",
                        column: x => x.PrimarySourceId,
                        principalTable: "PrimarySources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IcsrReports_Receivers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Receivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IcsrReports_Senders_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveSubstanceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveSubstances_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugReactionRelatedness",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugReactionAssesMeddraVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugReactionAsses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugAssessmentSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugReactionRelatedness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugReactionRelatedness_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConcomitantDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiseaseType = table.Column<int>(type: "int", nullable: true),
                    StartDateFormat = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Continuing = table.Column<int>(type: "int", nullable: true),
                    EndDateFormat = table.Column<int>(type: "int", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IcsrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcomitantDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcomitantDiseases_IcsrReports_IcsrId",
                        column: x => x.IcsrId,
                        principalTable: "IcsrReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConcomitantMedications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveIngredient = table.Column<int>(type: "int", nullable: true),
                    MedicationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateFormat = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDateFormat = table.Column<int>(type: "int", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IcsrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcomitantMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcomitantMedications_IcsrReports_IcsrId",
                        column: x => x.IcsrId,
                        principalTable: "IcsrReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstances_DrugId",
                table: "ActiveSubstances",
                column: "DrugId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConcomitantDiseases_IcsrId",
                table: "ConcomitantDiseases",
                column: "IcsrId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcomitantMedications_IcsrId",
                table: "ConcomitantMedications",
                column: "IcsrId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugReactionRelatedness_DrugId",
                table: "DrugReactionRelatedness",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_PatientId",
                table: "Drugs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_MessageHeaderId",
                table: "IcsrReports",
                column: "MessageHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_PatientId",
                table: "IcsrReports",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_PrimarySourceId",
                table: "IcsrReports",
                column: "PrimarySourceId",
                unique: true,
                filter: "[PrimarySourceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_ReceiverId",
                table: "IcsrReports",
                column: "ReceiverId",
                unique: true,
                filter: "[ReceiverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_SafetyReportId",
                table: "IcsrReports",
                column: "SafetyReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcsrReports_SenderId",
                table: "IcsrReports",
                column: "SenderId",
                unique: true,
                filter: "[SenderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_PatientId",
                table: "Reactions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_PatientId",
                table: "Summaries",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSubstances");

            migrationBuilder.DropTable(
                name: "ConcomitantDiseases");

            migrationBuilder.DropTable(
                name: "ConcomitantMedications");

            migrationBuilder.DropTable(
                name: "DrugReactionRelatedness");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Summaries");

            migrationBuilder.DropTable(
                name: "IcsrReports");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "MessageHeaders");

            migrationBuilder.DropTable(
                name: "PrimarySources");

            migrationBuilder.DropTable(
                name: "Receivers");

            migrationBuilder.DropTable(
                name: "Senders");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
