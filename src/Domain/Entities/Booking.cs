using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Domain.Entities
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime FinishTime { get; set; }

        public bool Available {  get; set; }

        public User? User { get; set; }
        
        [Required]
        public Court Court { get; set; }

        public Booking(DateTime startTime, DateTime finishTime, Court court)
        {
            StartTime = startTime;
            FinishTime = finishTime;
            Available = true;
            User = null;
            Court = court;
        }

        private Booking()
        {
            
        }
    }
}
