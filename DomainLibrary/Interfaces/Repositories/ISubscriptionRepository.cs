using DomainLibrary.Entities;
using DomainLibrary.Envelopes;

namespace DomainLibrary.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IResponse<Subscriptions>> InsertSubscription(Subscriptions subscriptionData);
        Task<IResponse<Subscriptions>> SelectOneSubscription(int subscriptionId);
        Task<IResponse<bool>> DeleteSubscription(int subscriptionId);
        Task<IResponse<List<Subscriptions>>> SelectAllSubscription();
    }
}
