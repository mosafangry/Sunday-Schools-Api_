using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.AuthEntities
{
    public class UserPermission
    {
        [Key]
        public int UserPermissionId { get; set; }

        public int UserId { get; set; }
        public int PermissionId { get; set; }

        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
