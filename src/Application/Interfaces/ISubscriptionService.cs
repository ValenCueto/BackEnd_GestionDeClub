using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISubscriptionService
    {
        List<Subscription> GetAll();
        Subscription? GetById(int id);
        void CreateSubscription(SubscriptionCreateRequest rq);
        void UpdateSubscription(SubscriptionCreateRequest rq, int subscriptionId);
        void DeleteSubscription(int subscriptionId);
    }
}
