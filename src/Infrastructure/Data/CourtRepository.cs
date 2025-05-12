using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CourtRepository : BaseRepository<Court>, ICourtRepository
    {
        public CourtRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
