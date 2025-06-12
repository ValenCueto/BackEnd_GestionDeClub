using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context) { }

        public bool Exists(int userId, int monthlyFeeId)
        {
            return _dbContext.Payments.Any(p =>
                p.UserId == userId && p.MonthlyFeeId == monthlyFeeId);
        }

        public Payment? GetByUserAndFee(int userId, int monthlyFeeId)
        {
            return _dbContext.Payments
                .Include(p => p.MonthlyFee)
                .Include(p => p.User)
                .FirstOrDefault(p => p.UserId == userId && p.MonthlyFeeId == monthlyFeeId);
        }

        public List<Payment> GetByUserId(int userId)
        {
            return _dbContext.Payments
                .Include(p => p.User)             
                .Include(p => p.MonthlyFee)       
                .Where(p => p.UserId == userId)
                .ToList();
        }

        public decimal GetCurrentMonthRevenue()
        {
            var now = DateTime.UtcNow;
            int currentMonth = now.Month;
            int currentYear = now.Year;

            return _dbContext.Payments
                .Where(p => p.Paid == true &&
                            p.MonthlyFee.Month == currentMonth &&
                            p.MonthlyFee.Year == currentYear)
                .Sum(p => (decimal)p.MonthlyFee.Price);
        }
    }
}
