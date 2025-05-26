using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        bool Exists(int userId, int monthlyFeeId);
        Payment? GetByUserAndFee(int userId, int monthlyFeeId);
        List<Payment> GetByUserId(int userId);
    }
}
