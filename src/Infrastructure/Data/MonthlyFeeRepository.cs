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
    public class MonthlyFeeRepository : BaseRepository<MonthlyFee>, IMonthlyFeeRepository
    {
        public MonthlyFeeRepository(ApplicationDbContext context) : base(context) { }

        public MonthlyFee? GetByMonthYear(int month, int year)
        {
            return _dbContext.MonthlyFees
                .FirstOrDefault(f => f.Month == month && f.Year == year);
        }
    }
}
