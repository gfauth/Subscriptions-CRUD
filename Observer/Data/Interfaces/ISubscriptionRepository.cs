using Observer.Data.Entities;
using Observer.Domain.ResponsesEnvelope;

namespace Observer.Data.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IResponse<Subscriptions>> InsertSubscription(Subscriptions subscriptionData);
        Task<IResponse<Subscriptions>> SelectOneSubscription(int subscriptionId);
        Task<IResponse<bool>> UpdateSubscription(Subscriptions subscriptionData);
        Task<IResponse<bool>> DeleteSubscription(int subscriptionId);
        Task<IResponse<List<Subscriptions>>> SelectAllSubscription();
    }
}
