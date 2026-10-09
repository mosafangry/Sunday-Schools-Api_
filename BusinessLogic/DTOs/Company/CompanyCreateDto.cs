using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Company
{
    public class CompanyCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string CompanyCode { get; set; }

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
    }

}
