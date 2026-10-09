using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class DrugReportDto
    {
        public string? MedicinalProduct { get; set; }
        public string? TradeName { get; set; }
        public string? CaseId { get; set; }
        public string? ReactionMeddraPT { get; set; }
        public string? NarrativeIncludeClinical { get; set; }
    }
    public class DrugReportRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? MedicinalProductFilter { get; set; }
        public string? TradeNameFilter { get; set; }
        public string? CaseIdFilter { get; set; }
        public string? ReactionMeddraPTFilter { get; set; }
        public string? NarrativeIncludeClinicalFilter { get; set; }
    }

    public class PagedDrugReportDto
    {
        public IEnumerable<DrugReportDto> Items { get; set; } = new List<DrugReportDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
