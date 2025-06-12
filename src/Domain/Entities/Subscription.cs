namespace Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public float Price {  get; set; }
        public DateOnly Month {  get; set; }
        public DateOnly Year { get; set; }
    }
}
