using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class MarkPaymentRequest
    {
        public int UserId { get; set; }
        public int MonthlyFeeId { get; set; }
    }
}
