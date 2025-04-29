using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class AvailabilityResponseDto
    {
        public string? DayOfWeek { get; set; } 
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }
        public int Duration { get; set; }
    }
}
