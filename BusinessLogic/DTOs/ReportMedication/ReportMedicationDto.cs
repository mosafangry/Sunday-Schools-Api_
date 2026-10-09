using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.ReportMedication
{
    public class ReportMedicationDto
    {
        public int Id { get; set; }
        public int? DrugCharacterization { get; set; }

        [Required(ErrorMessage = "Medicinal Product is required")]
        [StringLength(500, ErrorMessage = "Medicinal Product cannot exceed 500 characters")]
        public string? MedicinalProduct { get; set; }
        public string? TradeName { get; set; }

        //[StringLength(300, ErrorMessage = "Active Substance Name cannot exceed 300 characters")]
        //public string? ActiveSubstanceName { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string? ObtainDrugCountry { get; set; }

        [StringLength(200, ErrorMessage = "Administration Route cannot exceed 200 characters")]
        public string? DrugAdministrationRoute { get; set; }

        [StringLength(100, ErrorMessage = "Authorization Country cannot exceed 100 characters")]
        public string? DrugAuthorizationCountry { get; set; }
        [StringLength(20, ErrorMessage = "Meddra Version cannot exceed 20 characters")]
        public string? DrugIndicationMeddraVersion { get; set; }

        [StringLength(500, ErrorMessage = "Drug Indication cannot exceed 500 characters")]
        public string? DrugIndication { get; set; }

        public int? ActionDrug { get; set; }
        public int? DrugRecurReadministration { get; set; }
        // Audit fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateReportMedicationDto
    {
        [Required(ErrorMessage = "Medicinal Product is required")]
        [StringLength(500, ErrorMessage = "Medicinal Product cannot exceed 500 characters")]
        public string MedicinalProduct { get; set; } = string.Empty;
        public string? TradeName { get; set; }

        public int? DrugCharacterization { get; set; }

        //[StringLength(300, ErrorMessage = "Active Substance Name cannot exceed 300 characters")]
        //public string? ActiveSubstanceName { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string? ObtainDrugCountry { get; set; }

        [StringLength(200, ErrorMessage = "Administration Route cannot exceed 200 characters")]
        public string? DrugAdministrationRoute { get; set; }

        [StringLength(100, ErrorMessage = "Authorization Country cannot exceed 100 characters")]
        public string? DrugAuthorizationCountry { get; set; }

        [StringLength(20, ErrorMessage = "Meddra Version cannot exceed 20 characters")]
        public string? DrugIndicationMeddraVersion { get; set; }

        [StringLength(500, ErrorMessage = "Drug Indication cannot exceed 500 characters")]
        public string? DrugIndication { get; set; }

        public int? ActionDrug { get; set; }
        public int? DrugRecurReadministration { get; set; }

        //[Required(ErrorMessage = "Patient ID is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Patient ID must be greater than 0")]
        // public int? PatientId { get; set; }
    }

    public class UpdateReportMedicationDto
    {
        [Required(ErrorMessage = "Drug ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Drug ID must be greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Medicinal Product is required")]
        [StringLength(500, ErrorMessage = "Medicinal Product cannot exceed 500 characters")]
        public string MedicinalProduct { get; set; } = string.Empty;
        public string? TradeName { get; set; }

        public int? DrugCharacterization { get; set; }

        [StringLength(300, ErrorMessage = "Active Substance Name cannot exceed 300 characters")]
        public string? ActiveSubstanceName { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string? ObtainDrugCountry { get; set; }

        [StringLength(200, ErrorMessage = "Administration Route cannot exceed 200 characters")]
        public string? DrugAdministrationRoute { get; set; }

        [StringLength(100, ErrorMessage = "Authorization Country cannot exceed 100 characters")]
        public string? DrugAuthorizationCountry { get; set; }

      

        [StringLength(20, ErrorMessage = "Meddra Version cannot exceed 20 characters")]
        public string? DrugIndicationMeddraVersion { get; set; }

        [StringLength(500, ErrorMessage = "Drug Indication cannot exceed 500 characters")]
        public string? DrugIndication { get; set; }

        public int? ActionDrug { get; set; }
        public int? DrugRecurReadministration { get; set; }

        //[Required(ErrorMessage = "Patient ID is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Patient ID must be greater than 0")]
        //public int PatientId { get; set; }
    }

    public class ReportMedicationSummaryDto
    {
        public int Id { get; set; }
        public string? MedicinalProduct { get; set; }
        public string? TradeName { get; set; }

        public string? ActiveSubstanceName { get; set; }
        public int? DrugCharacterization { get; set; }
        public string? DrugStartDate { get; set; }
        public string? DrugEndDate { get; set; }
        //    public int? PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
