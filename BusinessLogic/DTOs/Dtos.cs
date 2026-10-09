//// DTOs/UserDtos.cs
//using System.ComponentModel.DataAnnotations;

//namespace BusinessLogic
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
//        [StringLength(100, MinimumLength = 3)]
//        public string Username { get; set; }

//        [Required]
//        [EmailAddress]
//        [StringLength(255)]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100, MinimumLength = 6)]
//        public string Password { get; set; }

//        [StringLength(100)]
//        public string FirstName { get; set; }

//        [StringLength(100)]
//        public string LastName { get; set; }

//        public bool IsActive { get; set; } = true;
//        public List<int> RoleIds { get; set; } = new List<int>();
//    }

//    public class UpdateUserDto
//    {
//        [Required]
//        [StringLength(100, MinimumLength = 3)]
//        public string Username { get; set; }

//        [Required]
//        [EmailAddress]
//        [StringLength(255)]
//        public string Email { get; set; }

//        [StringLength(100)]
//        public string FirstName { get; set; }

//        [StringLength(100)]
//        public string LastName { get; set; }

//        public bool IsActive { get; set; }
//        public List<int> RoleIds { get; set; } = new List<int>();
//    }

//    public class ChangePasswordDto
//    {
//        [Required]
//        public string CurrentPassword { get; set; }

//        [Required]
//        [StringLength(100, MinimumLength = 6)]
//        public string NewPassword { get; set; }

//        [Required]
//        [Compare("NewPassword")]
//        public string ConfirmPassword { get; set; }
//    }

//    public class LoginDto
//    {
//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        public string Password { get; set; }
//    }

//    public class UserPermissionDto
//    {
//        public int UserPermissionId { get; set; }
//        public int UserId { get; set; }
//        public int PermissionId { get; set; }
//        public string PermissionName { get; set; }
//        public DateTime GrantedAt { get; set; }
//        public DateTime? ExpiresAt { get; set; }
//    }

//    public class AssignUserPermissionDto
//    {
//        [Required]
//        public int PermissionId { get; set; }
//        public DateTime? ExpiresAt { get; set; }
//    }
//}

//// DTOs/RoleDtos.cs

//namespace BusinessLogic
//{
//    public class RoleDto
//    {
//        public int RoleId { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public bool IsActive { get; set; }
//        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
//    }

//    public class CreateRoleDto
//    {
//        [Required]
//        [StringLength(50, MinimumLength = 2)]
//        public string Name { get; set; }

//        [StringLength(255)]
//        public string Description { get; set; }

//        public bool IsActive { get; set; } = true;
//        public List<int> PermissionIds { get; set; } = new List<int>();
//    }

//    public class UpdateRoleDto
//    {
//        [Required]
//        [StringLength(50, MinimumLength = 2)]
//        public string Name { get; set; }

//        [StringLength(255)]
//        public string Description { get; set; }

//        public bool IsActive { get; set; }
//        public List<int> PermissionIds { get; set; } = new List<int>();
//    }
//}

//// DTOs/PermissionDtos.cs

//namespace BusinessLogic
//{
//    public class PermissionDto
//    {
//        public int PermissionId { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public string Category { get; set; }
//        public bool IsActive { get; set; }
//    }

//    public class CreatePermissionDto
//    {
//        [Required]
//        [StringLength(100, MinimumLength = 2)]
//        public string Name { get; set; }

//        [StringLength(255)]
//        public string Description { get; set; }

//        [StringLength(50)]
//        public string Category { get; set; }

//        public bool IsActive { get; set; } = true;
//    }

//    public class UpdatePermissionDto
//    {
//        [Required]
//        [StringLength(100, MinimumLength = 2)]
//        public string Name { get; set; }

//        [StringLength(255)]
//        public string Description { get; set; }

//        [StringLength(50)]
//        public string Category { get; set; }

//        public bool IsActive { get; set; }
//    }
//}

//// DTOs/SubscriptionDtos.cs

//namespace BusinessLogic
//{
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

//    public class CreateSubscriptionDto
//    {
//        [Required]
//        [StringLength(50, MinimumLength = 2)]
//        public string Name { get; set; }

//        [StringLength(255)]
//        public string Description { get; set; }

//        [Required]
//        [Range(0.01, double.MaxValue)]
//        public decimal Price { get; set; }

//        [Required]
//        [Range(1, int.MaxValue)]
//        public int DurationInDays { get; set; }

//        public bool IsActive { get; set; } = true;
//        public List<int> PermissionIds { get; set; } = new List<int>();
//    }

//    public class UpdateSubscriptionDto
//    {
//        [Required]
//        [StringLength(50, MinimumLength = 2)]
//        public string Name { get; set; }

//        [StringLength(255)]
//        public string Description { get; set; }

//        [Required]
//        [Range(0.01, double.MaxValue)]
//        public decimal Price { get; set; }

//        [Required]
//        [Range(1, int.MaxValue)]
//        public int DurationInDays { get; set; }

//        public bool IsActive { get; set; }
//        public List<int> PermissionIds { get; set; } = new List<int>();
//    }

//    public class UserSubscriptionDto
//    {
//        public int UserSubscriptionId { get; set; }
//        public int UserId { get; set; }
//        public int SubscriptionId { get; set; }
//        public string SubscriptionName { get; set; }
//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }
//        public bool IsActive { get; set; }
//        public bool IsPaid { get; set; }
//        public string PaymentTransactionId { get; set; }
//        public SubscriptionDto Subscription { get; set; }
//    }

//    public class CreateUserSubscriptionDto
//    {
//        [Required]
//        public int SubscriptionId { get; set; }

//        [Required]
//        public string PaymentTransactionId { get; set; }
//    }
//}

//// DTOs/AuthDtos.cs
//namespace BusinessLogic
//{
//    public class AuthResponseDto
//    {
//        public string Token { get; set; }
//        public DateTime Expires { get; set; }
//        public UserDto User { get; set; }
//        public List<string> Permissions { get; set; } = new List<string>();
//    }

//    public class RefreshTokenDto
//    {
//        public string Token { get; set; }
//        public string RefreshToken { get; set; }
//    }

//    public class JwtConfigDto
//    {
//        public string SecretKey { get; set; }
//        public string Issuer { get; set; }
//        public string Audience { get; set; }
//        public int ExpirationInMinutes { get; set; }
//    }
//}

//// DTOs/Common/ResponseDtos.cs
//namespace BusinessLogic.Common
//{
//    public class ApiResponse<T>
//    {
//        public bool Success { get; set; }
//        public string Message { get; set; }
//        public T Data { get; set; }
//        public List<string> Errors { get; set; } = new List<string>();

//        public static ApiResponse<T> SuccessResult(T data, string message = "Success")
//        {
//            return new ApiResponse<T>
//            {
//                Success = true,
//                Message = message,
//                Data = data
//            };
//        }

//        public static ApiResponse<T> ErrorResult(string message, List<string> errors = null)
//        {
//            return new ApiResponse<T>
//            {
//                Success = false,
//                Message = message,
//                Errors = errors ?? new List<string>()
//            };
//        }
//    }

//    public class PagedResponse<T>
//    {
//        public List<T> Items { get; set; } = new List<T>();
//        public int TotalCount { get; set; }
//        public int Page { get; set; }
//        public int PageSize { get; set; }
//        public int TotalPages { get; set; }
//        public bool HasPrevious { get; set; }
//        public bool HasNext { get; set; }
//    }

//    public class PaginationParameters
//    {
//        private int _pageSize = 10;
//        private const int MaxPageSize = 100;

//        public int Page { get; set; } = 1;

//        public int PageSize
//        {
//            get => _pageSize;
//            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
//        }

//        public string SearchTerm { get; set; }
//        public string SortBy { get; set; }
//        public bool SortDescending { get; set; } = false;
//    }
//}