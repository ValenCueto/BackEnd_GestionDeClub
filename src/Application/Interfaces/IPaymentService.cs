using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        void AssignFeeToAllUsers(int monthlyFeeId);
        void AssignFeeToUser(int userId, int monthlyFeeId);
        void MarkAsPaid(MarkPaymentRequest request);
        List<PaymentDtoResponse> GetByUserId(int userId);
        decimal GetCurrentMonthRevenue();
    }
}
