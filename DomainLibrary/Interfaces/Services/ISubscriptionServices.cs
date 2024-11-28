using DomainLibrary.Envelopes;
using DomainLibrary.Models.Requests;
using DomainLibrary.Models.Responses;

namespace DomainLibrary.Interfaces.Services
{
    public interface ISubscriptionServices
    {
        public Task<IResponse<ResponseEnvelope>> RetrieveSubscription(int subscriptionId);
        public Task<IResponse<ResponseEnvelope>> CreateSubscription(SubscriptionRequest subscription);
        public Task<IResponse<ResponseEnvelope>> DeleteSubscription(int subscriptionId);

        //public Task<IResponse<ResponseEnvelope>> ListSubscription();
    }
}
