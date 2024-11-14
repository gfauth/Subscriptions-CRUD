using Observer.Domain.Models.Requests;

namespace Observer.Data.Entities
{
    public class Subscriptions
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Subscriptions() { }

        /// <summary>
        /// SubscriptionData Constructor for use when a new subscription will be inserte into database.
        /// </summary>
        /// <param name="request">Object SubscriptionRequest who become from requester.</param>
        public Subscriptions(SubscriptionRequest request)
        {
            ProductId = request.ProductId;
            UserId = request.UserId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// SubscriptionData Constructor for use when need to edit a subscription data into database.
        /// </summary>
        /// <param name="subscriptionId">Subscription identification.</param>
        /// <param name="request">Object Subscriptions who become from database.</param>
        public Subscriptions(int subscriptionId, Subscriptions request)
        {
            Id = subscriptionId;
            ProductId = request.ProductId;
            UserId = request.UserId;
            CreatedAt = request.CreatedAt;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// SubscriptionData Constructor for use when need to edit an subscription data into database.
        /// </summary>
        /// <param name="subscriptionId">Subscription identification.</param>
        /// <param name="request">Object SubscriptionRequest who become from requester.</param>
        public Subscriptions(int subscriptionId, SubscriptionRequest request)
        {
            Id = subscriptionId;
            ProductId = request.ProductId;
            UserId = request.UserId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}