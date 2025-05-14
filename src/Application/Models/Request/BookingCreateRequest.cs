using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class BookingCreateRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
