using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
