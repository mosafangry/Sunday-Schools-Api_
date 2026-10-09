//using System.ComponentModel.DataAnnotations;
//using BusinessLogic.DTOs;

//namespace BusinessLogic.DTOs
//{
//    public class UserDto
//    {
//        public int UserId { get; set; }
//        public string Username { get; set; }
//        public string Email { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public bool IsActive { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public DateTime? LastLoginAt { get; set; }
//        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
//        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
//        public UserSubscriptionDto Subscription { get; set; }
//    }

//    public class CreateUserDto
//    {
//        [Required]
//        [MaxLength(100)]
//        public string Username { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [MinLength(6)]
//        public string Password { get; set; }

//        [MaxLength(100)]
//        public string FirstName { get; set; }

//        [MaxLength(100)]
//        public string LastName { get; set; }

//        public List<int> RoleIds { get; set; } = new List<int>();
//    }

//    public class UpdateUserDto
//    {
//        [MaxLength(100)]
//        public string FirstName { get; set; }

//        [MaxLength(100)]
//        public string LastName { get; set; }

//        public bool? IsActive { get; set; }
//        public List<int> RoleIds { get; set; } = new List<int>();
//    }

//    public class LoginDto
//    {
//        [Required]
//        public string EmailOrUsername { get; set; }

//        [Required]
//        public string Password { get; set; }
//    }

//    public class AuthResponseDto
//    {
//        public string Token { get; set; }
//        public DateTime Expires { get; set; }
//        public UserDto User { get; set; }
//    }

//    public class RoleDto
//    {
//        public int RoleId { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public bool IsActive { get; set; }
//        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
//    }

//    public class PermissionDto
//    {
//        public int PermissionId { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public string Category { get; set; }
//        public bool IsActive { get; set; }
//    }

//    public class UserSubscriptionDto
//    {
//        public int UserSubscriptionId { get; set; }
//        public SubscriptionDto Subscription { get; set; }
//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }
//        public bool IsActive { get; set; }
//        public bool IsPaid { get; set; }
//    }

//    public class SubscriptionDto
//    {
//        public int SubscriptionId { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public decimal Price { get; set; }
//        public int DurationInDays { get; set; }
//        public bool IsActive { get; set; }
//        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
//    }

//    public class AssignRoleDto
//    {
//        [Required]
//        public int UserId { get; set; }
//        [Required]
//        public int RoleId { get; set; }
//    }

//    public class AssignPermissionDto
//    {
//        [Required]
//        public int UserId { get; set; }
//        [Required]
//        public int PermissionId { get; set; }
//        public DateTime? ExpiresAt { get; set; }
//    }
//}
