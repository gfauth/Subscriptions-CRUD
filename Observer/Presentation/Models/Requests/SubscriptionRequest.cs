namespace Observer.Presentation.Models.Requests
{
    public record SubscriptionRequest
    {
        public int ProductId { get; private set; }
        public int UserId { get; private set; }
    }
}