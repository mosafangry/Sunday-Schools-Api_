using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.User
{
    public class UserSubscriptionDto
    {
        public int UserSubscriptionId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentTransactionId { get; set; }
    }

}
