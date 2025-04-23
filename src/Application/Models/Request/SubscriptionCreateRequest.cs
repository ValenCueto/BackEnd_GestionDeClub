namespace Application.Models.Request
{
    public class SubscriptionCreateRequest
    {
        public float Price { get; set; }
        public DateOnly Month { get; set; }
        public DateOnly Year { get; set; }
    }
}
