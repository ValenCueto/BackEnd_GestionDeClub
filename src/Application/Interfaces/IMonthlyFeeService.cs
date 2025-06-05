using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMonthlyFeeService
    {
        void Create(MonthlyFeeCreateRequest request);
        void Update(MonthlyFeeUpdateRequest request, int monthlyFeeId);
        void Delete(int monthlyFeeId);
        List<MonthlyFeeDtoResponse> GetAll();
        MonthlyFeeDtoResponse? GetByMonthYear(int month, int year);
    }
}
