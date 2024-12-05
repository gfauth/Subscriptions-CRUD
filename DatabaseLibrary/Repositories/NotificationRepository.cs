using System.Diagnostics.CodeAnalysis;
using DatabaseLibrary.Constants;
using DomainLibrary.Constants;
using DomainLibrary.Entities;
using DomainLibrary.Envelopes;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Models.LogModels;
using DomainLibrary.Models.Notifications;
using SingleLog.Interfaces;

namespace DatabaseLibrary.Repositories
{
    /// <summary>
    /// Repository for entity Subscriptions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NotificationRepository : INotificationRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// Repository constructor.
        /// </summary>
        /// <param name="sqlServerContext">Object ISqlServerContext.</param>
        /// <param name="singleLog">Object ISingleLog.</param>
        public NotificationRepository(ISqlServerContext sqlServerContext, ISingletonLogger<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        public async Task<IResponse<List<Subscriptions>>> FindSubscriptions() =>
            await QueryRunner<Subscriptions>.ExecuteQueryListTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, Queries.SelectAllSubscriptions, null);

        public async Task<IResponse<List<Notifications>>> FindNotifications() =>
            await QueryRunner<Notifications>.ExecuteQueryListTAsync(_singleLog, LogSteps.NOTIFICATION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, Queries.SelectOneSubscriptions, null);
    }
}