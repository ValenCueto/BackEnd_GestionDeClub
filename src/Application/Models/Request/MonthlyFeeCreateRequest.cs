using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class MonthlyFeeCreateRequest
    {
        public float Price { get; set; }
        public int Month { get; set; }  
        public int Year { get; set; }   
    }   
}
