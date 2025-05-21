using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MonthlyFee
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public int Month { get; set; }       // 1 a 12
        public int Year { get; set; }        // Ej: 2025
        public DateTime DueDate { get; set; } // Siempre el 10 del mes

        // Relación con Payments (opcional, si luego se navega desde MonthlyFee a Payments)
       //public List<Payment> Payments { get; set; }
    }
}
