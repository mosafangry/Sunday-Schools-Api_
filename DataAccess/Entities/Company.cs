using DataAccess.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Company : AuditableEntity
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string CompanyCode { get; set; } // Unique identifier

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string ContactPerson { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }

        // Navigation properties
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<IcsrEntity> IcsrReports { get; set; } = new List<IcsrEntity>();
        public virtual ICollection<DrugEntity> DrugEntities { get; set; } = new List<DrugEntity>();
    }
}
