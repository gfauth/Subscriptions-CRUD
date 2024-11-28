using System.Diagnostics.CodeAnalysis;
using DatabaseLibrary.Constants;
using DomainLibrary.Constants;
using DomainLibrary.Entities;
using DomainLibrary.Envelopes;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Models.LogModels;
using SingleLog.Interfaces;

namespace DatabaseLibrary.Repositories
{
    /// <summary>
    /// Repository for entity Subscriptions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// Repository constructor.
        /// </summary>
        /// <param name="sqlServerContext">Object ISqlServerContext.</param>
        /// <param name="singleLog">Object ISingleLog.</param>
        public SubscriptionRepository(ISqlServerContext sqlServerContext, ISingletonLogger<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        /// <summary>
        /// Insert new data into Products table.
        /// </summary>
        /// <param name="subscriptionData">Object Products</param>
        /// <returns></returns>
        public async Task<IResponse<Subscriptions>> InsertSubscription(Subscriptions subscriptionData) =>
            await QueryRunner<Subscriptions>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_DELETE_DATA,
            _sqlServerContext, Queries.InsertSubscriptions, subscriptionData);

        /// <summary>
        /// Select an existent into Products table.
        /// </summary>
        /// <param name="subscriptionId">Products identification</param>
        /// <returns></returns>
        public async Task<IResponse<Subscriptions>> SelectOneSubscription(int subscriptionId) =>
            await QueryRunner<Subscriptions>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, Queries.SelectOneSubscriptions, new { subscriptionId });

        /// <summary>
        /// Select all existent into Products table.
        /// </summary>
        /// <returns></returns>
        public async Task<IResponse<List<Subscriptions>>> SelectAllSubscription() =>
            await QueryRunner<Subscriptions>.ExecuteQueryListTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, Queries.SelectOneSubscriptions, null);

        /// <summary>
        /// Delete data existent into Products table.
        /// </summary>
        /// <param name="subscriptionId">Products identification</param>
        /// <returns></returns>
        public async Task<IResponse<bool>> DeleteSubscription(int subscriptionId) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_DELETE_DATA,
            _sqlServerContext, Queries.DeleteSubscriptions, new { subscriptionId });
    }
}