using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   
    public class ReportMedication : AuditableEntity, ITrackable
    {
        [Key]
        public int Id { get; set; }
        public string? MedicinalProduct { get; set; }
        public string? TradeName { get; set; }
        public int? DrugCharacterization { get; set; }
        public string? ActiveSubstanceName { get; set; }
        public string? ObtainDrugCountry { get; set; }
        public string? DrugAdministrationRoute { get; set; }
        public string? DrugAuthorizationCountry { get; set; }       
        public string? DrugIndication { get; set; }
        public int? CompanyId { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
    }

}
