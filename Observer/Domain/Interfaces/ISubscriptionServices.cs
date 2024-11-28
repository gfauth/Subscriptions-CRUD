using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using Observer.Domain.ResponsesEnvelope;

namespace Observer.Domain.Interfaces
{
    public interface ISubscriptionServices
    {
        public Task<IResponse<ResponseEnvelope>> RetrieveSubscription(int subscriptionId);
        public Task<IResponse<ResponseEnvelope>> CreateSubscription(SubscriptionRequest subscription);
        public Task<IResponse<ResponseEnvelope>> DeleteSubscription(int subscriptionId);

        //public Task<IResponse<ResponseEnvelope>> ListSubscription();
    }
}
