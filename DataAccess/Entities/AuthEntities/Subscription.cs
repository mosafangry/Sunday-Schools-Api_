using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.AuthEntities
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } // Basic, Premium, Enterprise

        [MaxLength(255)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int DurationInDays { get; set; } // 30, 365, etc.

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
        public virtual ICollection<SubscriptionPermission> SubscriptionPermissions { get; set; } = new List<SubscriptionPermission>();
    }
}
