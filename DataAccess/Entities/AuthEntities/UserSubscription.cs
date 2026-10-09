using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.AuthEntities
{
    public class UserSubscription
    {
        [Key]
        public int UserSubscriptionId { get; set; }

        public int UserId { get; set; }
        public int SubscriptionId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsPaid { get; set; } = false;

        [MaxLength(100)]
        public string PaymentTransactionId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
