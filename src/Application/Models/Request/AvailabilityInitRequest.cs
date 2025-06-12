using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class AvailabilityInitRequest
    {
        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly FinishTime { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
