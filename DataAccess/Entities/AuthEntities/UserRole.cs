using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.AuthEntities
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
