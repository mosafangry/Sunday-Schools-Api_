using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.AuthEntities
{
    public class SubscriptionPermission
    {
        [Key]
        public int SubscriptionPermissionId { get; set; }

        public int SubscriptionId { get; set; }
        public int PermissionId { get; set; }

        // Navigation properties
        public virtual Subscription Subscription { get; set; }
        public virtual Permission Permission { get; set; }
    }
}