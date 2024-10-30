using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;

namespace Observer.Domain.Interfaces
{
    public interface ISubscriptionServices
    {
        public Task<IResponse<ResponseEnvelope>> RetrieveSubscription(int subscriptionId);
        public Task<IResponse<ResponseEnvelope>> CreateSubscription(SubscriptionRequest subscription);
        public Task<IResponse<ResponseEnvelope>> UpdateSubscription(int subscriptionId, SubscriptionRequest subscription);
        public Task<IResponse<ResponseEnvelope>> DeleteSubscription(int subscriptionId);

        //public Task<IResponse<ResponseEnvelope>> ListSubscription();
    }
}
