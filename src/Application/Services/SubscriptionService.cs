using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public List<Subscription> GetAll()
        {
            return _subscriptionRepository.GetAll();
        }

        public Subscription? GetById(int id)
        {
            return _subscriptionRepository.GetById(id);
        }

        public void CreateSubscription(SubscriptionCreateRequest rq)
        {
            var subscription = new Subscription()
            {
                Price = rq.Price,
                Month = rq.Month,
                Year = rq.Year,
            };

            _subscriptionRepository.Add(subscription);
        }

        public void UpdateSubscription(SubscriptionCreateRequest rq, int subscriptionId)
        {
            var subscription = _subscriptionRepository.GetById(subscriptionId);

            if(subscription != null)
            {
                subscription.Price = rq.Price;
                subscription.Month = rq.Month;
                subscription.Year = rq.Year;

                _subscriptionRepository.Update(subscription);
            }
        }

        public void DeleteSubscription(int subscriptionId)
        {
            var subscription = _subscriptionRepository.GetById(subscriptionId);

            if (subscription != null)
            {
                _subscriptionRepository.Delete(subscription);
            }
        }
    }
}
