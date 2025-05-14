using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public List<News> GetByDate(DateTime date)
        {
            return _dbContext.News
                .Where(n => n.Date.Date == date.Date) 
                .ToList();
        }

    }
}
