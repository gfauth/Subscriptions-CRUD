using Dapper;
using Microsoft.Data.SqlClient;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace Observer.Data
{
    public static class QueryExecuter<T>
    {
        public static async Task<IResponse<T>> ExecuteQuerySingleTAsync(ISingleLog<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, object parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StopwatchStart();

            try
            {
                await using SqlConnection connection = await sqlServerContext.GetConnection();

                var response = await connection.QuerySingleAsync<T>(query, parameters);

                return new ResponseOk<T>(response);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;

                if (ex.Message.Equals("Sequence contains no elements"))
                    return new ResponseError<T>("No data found.");

                throw;
            }
            finally
            {
                sublog.StopwatchStop();

                await sqlServerContext.DisposeAsync();
            }
        }
     
        public static async Task<IResponse<List<T>>> ExecuteQueryListTAsync(ISingleLog<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, object parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StopwatchStart();

            try
            {
                await using SqlConnection connection = await sqlServerContext.GetConnection();

                var response = await connection.QueryAsync<T>(query, parameters);

                return new ResponseOk<List<T>>(response.ToList());
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;

                if (ex.Message.Equals("Sequence contains no elements"))
                    return new ResponseError<List<T>>("No data found.");

                throw;
            }
            finally
            {
                sublog.StopwatchStop();

                await sqlServerContext.DisposeAsync();
            }
        }
    }
}
