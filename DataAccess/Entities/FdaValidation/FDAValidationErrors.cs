using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.FdaValidation
{
    public class FDAValidationErrors: AuditableEntity
    {
        public int Id { get; set; }
        public string TagId { get; set; }
        public string TagLabel { get; set; }
        public string OriginalXmlValue { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
        public int FDAValidationResultId { get; set; }
        public FDAValidationResult FDAValidationResult { get; set; }
    }
}
