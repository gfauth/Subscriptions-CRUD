namespace Observer.Presentation.Models.Responses
{
    public class SubscriptionsEnvelope
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
