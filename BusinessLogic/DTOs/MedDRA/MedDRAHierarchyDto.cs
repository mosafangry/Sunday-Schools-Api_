using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.MedDRA
{
    public class MedDRAHierarchyDto
    {
        public string SOC_Code { get; set; }
        public string SOC_Name { get; set; }
        public string HLGT_Code { get; set; }
        public string HLGT_Name { get; set; }
        public string HLT_Code { get; set; }
        public string HLT_Name { get; set; }
        public string PT_Code { get; set; }
        public string PT_Name { get; set; }
        public string LLT_Code { get; set; }
        public string LLT_Name { get; set; }
        public bool IsPrimarySOC { get; set; }
    }

    public class MedDRASearchRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string SearchTerm { get; set; }

        public bool PrimarySOCOnly { get; set; } = true;

        [Range(1, 1000)]
        public int MaxResults { get; set; } = 100;
    }

    public class MedDRASearchResponse
    {
        public List<MedDRAHierarchyDto> Results { get; set; } = new();
        public int TotalCount { get; set; }
        public string SearchTerm { get; set; }
        public bool HasMore { get; set; }
    }

    public class MedDRATermRequest
    {
        [Required]
        public string TermCode { get; set; }

        [Required]
        public string TermLevel { get; set; } = "LLT"; // LLT, PT, HLT, HLGT, SOC
    }

    public class MedDRASOCRequest
    {
        [Required]
        public string SOCCode { get; set; }

        public bool PrimarySOCOnly { get; set; } = true;
    }
    public class MedDRASearchUnifiedRequest
    {
        public string? TermCode { get; set; }

        public string? TermLevel { get; set; } // Required with TermCode

        [StringLength(100, MinimumLength = 2)]
        public string? SearchTerm { get; set; }

        public bool PrimarySOCOnly { get; set; } = true;

        [Range(1, 1000)]
        public int MaxResults { get; set; } = 100;
    }
    public class AdverseEventRecord
    {
        public string MedicationName { get; set; }
        public string PT_Code { get; set; }
    }

    public class MedicationSignalDto
    {
        public string PT_Code { get; set; }
        public string PT_Name { get; set; }
        public string SOC_Name { get; set; }
        public int CaseCount { get; set; }
        public double PRR { get; set; }
        public bool SignalDetected { get; set; }
    }
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string TermCode { get; set; }
        public string TermLevel { get; set; }
        public string Message { get; set; }
    }
    // Request and response DTOs for the endpoint
    public class MedDRASignalRequest
    {
        public string MedicationName { get; set; }
        public bool PrimarySOCOnly { get; set; }
        public int MaxResults { get; set; }
    }

    public class MedDRASignalResponse
    {
        public string MedicationName { get; set; }
        public List<MedicationSignalDto> Results { get; set; }
        public int TotalResults { get; set; }
    }

}
