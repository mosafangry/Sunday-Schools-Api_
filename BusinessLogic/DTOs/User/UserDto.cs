using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int? CompanyId { get; set; }
        public virtual CompanyDto? Company { get; set; }
        public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public ICollection<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
        public UserSubscriptionDto Subscription { get; set; }
        public string PhoneNumber { get; set; }

    }

}
