using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MonthlyFeeId { get; set; }
        public MonthlyFee MonthlyFee { get; set; }

        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
