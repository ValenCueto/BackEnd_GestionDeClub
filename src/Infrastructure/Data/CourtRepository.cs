using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class CourtRepository : BaseRepository<Court>, ICourtRepository
    {
        public CourtRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
