using System.Net;
using AutoMapper;
using DomainLibrary.Constants;
using DomainLibrary.Entities;
using DomainLibrary.Envelopes;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Interfaces.Services;
using DomainLibrary.Models.Errors;
using DomainLibrary.Models.LogModels;
using DomainLibrary.Models.Requests;
using DomainLibrary.Models.Responses;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace Observer.Services
{
    public class SubscriptionServices : ISubscriptionServices
    {
        private readonly ISingletonLogger<LogModel> _singleLog;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly MapperConfiguration _mapperConfiguration;

        /// <summary>
        /// Users services constructor.
        /// </summary>
        /// <param name="subscriptionRepository">Service class of repository based on ISubscriptionRepository.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public SubscriptionServices(ISubscriptionRepository subscriptionRepository, ISingletonLogger<LogModel> singleLog)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _singleLog = singleLog ?? throw new ArgumentNullException(nameof(singleLog));

            _mapperConfiguration = new MapperConfiguration(config => { config.CreateMap<Subscriptions, SubscriptionsEnvelope>(); });
        }

        public async Task<IResponse<ResponseEnvelope>> CreateSubscription(SubscriptionRequest subscription)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.SUBSCRIPTION_SERVICE_CREATE_SUBSCRIPTION_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                Subscriptions subscriptionData = new Subscriptions(subscription);

                var result = await _subscriptionRepository.InsertSubscription(subscriptionData);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(SubscriptionResponseErrors.CreateSubscriptionError);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.Created, $"Subscrição {result.Data.Id} criada com sucesso.", mapper.Map<SubscriptionsEnvelope>(result.Data));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> RetrieveSubscription(int subscriptionId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.SUBSCRIPTION_SERVICE_RETRIEVE_SUBSCRIPTION_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                var result = await _subscriptionRepository.SelectOneSubscription(subscriptionId);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(SubscriptionResponseErrors.SubscriptionNotFound);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.OK, "Subscrição recuperada com sucesso.", mapper.Map<SubscriptionsEnvelope>(result.Data));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> DeleteSubscription(int subscriptionId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.SUBSCRIPTION_SERVICE_DELETE_SUBSCRIPTION_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                var result = await _subscriptionRepository.DeleteSubscription(subscriptionId);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(SubscriptionResponseErrors.SubscriptionNotFound);

                return new ResponseOk<ResponseEnvelope>(new ResponseEnvelope(HttpStatusCode.OK, $"Subscrição {subscriptionId} foi deletada com sucesso."));
            }
            finally
            {
                sublog.StopCronometer();
            }
        }
    }
}