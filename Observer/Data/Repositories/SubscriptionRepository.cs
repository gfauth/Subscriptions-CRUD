using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;

namespace Observer.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
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
            await QueryRunner<Subscriptions, Subscriptions>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_DELETE_DATA,
            _sqlServerContext, QueryData.InsertSubscriptions, subscriptionData);

        /// <summary>
        /// Select an existent into Products table.
        /// </summary>
        /// <param name="subscriptionId">Products identification</param>
        /// <returns></returns>
        public async Task<IResponse<Subscriptions>> SelectOneSubscription(int subscriptionId) =>
            await QueryRunner<Subscriptions, int>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, QueryData.SelectOneSubscriptions, subscriptionId);

        /// <summary>
        /// Select all existent into Products table.
        /// </summary>
        /// <returns></returns>
        public async Task<IResponse<List<Subscriptions>>> SelectAllSubscription() =>
            await QueryRunner<Subscriptions, object>.ExecuteQueryListTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, QueryData.SelectOneSubscriptions, null);

        /// <summary>
        /// Update data in existent into Products table.
        /// </summary>
        /// <param name="subscriptionData">Object Products</param>
        /// <returns></returns>
        public async Task<IResponse<bool>> UpdateSubscription(Subscriptions subscriptionData) =>
            await QueryRunner<bool, Subscriptions>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_UPDATE_DATA,
            _sqlServerContext, QueryData.UpdateSubscriptions, subscriptionData);

        /// <summary>
        /// Delete data existent into Products table.
        /// </summary>
        /// <param name="subscriptionId">Products identification</param>
        /// <returns></returns>
        public async Task<IResponse<bool>> DeleteSubscription(int subscriptionId) =>
            await QueryRunner<bool, int>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_DELETE_DATA,
            _sqlServerContext, QueryData.DeleteSubscriptions, subscriptionId);
    }
}