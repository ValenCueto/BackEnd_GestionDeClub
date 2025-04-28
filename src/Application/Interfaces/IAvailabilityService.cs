using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAvailabilityService
    {
        void InitDefaultAvailability(AvailabilityInitRequest dto);
        List<AvailabilityResponseDto> GetAll();
        void UpdateAvailability(AvailabilityInitRequest dto, string day);
    }
}
