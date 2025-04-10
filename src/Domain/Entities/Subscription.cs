using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public class Subscription
    {
 
        public int Id { get; set; }
        public float Price {  get; set; }
        public Date Month {  get; set; }
        public Date Year { get; set; }

    }
}
