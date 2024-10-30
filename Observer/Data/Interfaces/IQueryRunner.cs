using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;

namespace Observer.Data.Interfaces
{
    public interface IQueryRunner<T>
    {
        public Task<IResponse<T>> ExecuteQuerySingleTAsync(ISingletonLogger<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, object parameters);
        public Task<IResponse<List<T>>> ExecuteQueryListTAsync(ISingletonLogger<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, object parameters);
    }
}
