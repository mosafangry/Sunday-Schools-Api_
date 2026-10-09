using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.AuthEntities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
        public virtual UserSubscription UserSubscription { get; set; }

        // Foreign key for Company
        // [Required]
        public int? CompanyId { get; set; }

        // Navigation properties
        public virtual Company? Company { get; set; }

        [Required]
        [MaxLength(255)]
        public string PhoneNumber { get; set; }
    }
}
