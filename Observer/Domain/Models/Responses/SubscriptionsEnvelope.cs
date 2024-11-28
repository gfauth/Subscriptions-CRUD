namespace Observer.Domain.Models.Responses
{
    /// <summary>
    /// Subscription data response envelope.
    /// </summary>
    public record SubscriptionsEnvelope
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public SubscriptionsEnvelope()
        {

        }
    }
}
