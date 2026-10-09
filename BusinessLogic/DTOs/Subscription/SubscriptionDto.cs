using BusinessLogic.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Subscription
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; }
        public ICollection<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
