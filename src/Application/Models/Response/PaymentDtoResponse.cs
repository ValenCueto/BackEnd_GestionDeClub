using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class PaymentDtoResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int MonthlyFeeId { get; set; }
        public float Price { get; set; }
        public string Period { get; set; } // Ej: "Mayo 2025"
        public int Month { get; set; }     
        public int Year { get; set; }      
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
