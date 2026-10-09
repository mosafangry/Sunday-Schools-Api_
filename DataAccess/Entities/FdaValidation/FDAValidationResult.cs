using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.FdaValidation
{
    public class FDAValidationResult: AuditableEntity
    {
        public int Id { get; set; }
        public string StatusText { get; set; } = "";
        public string? SafetyReportId { get; set; }
        public List<FDAValidationErrors> Errors { get; set; } = new();
    }
}
