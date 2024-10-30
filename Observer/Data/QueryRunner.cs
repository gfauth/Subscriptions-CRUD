using Dapper;
using Microsoft.Data.SqlClient;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace Observer.Data
{
    public sealed class QueryRunner<T>
    {
        public static async Task<IResponse<T>> ExecuteQuerySingleTAsync(ISingletonLogger<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, object parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StartCronometer();

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
                sublog.StopCronometer();

                await sqlServerContext.DisposeAsync();
            }
        }
     
        public static async Task<IResponse<List<T>>> ExecuteQueryListTAsync(ISingletonLogger<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, object parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StartCronometer();

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
                sublog.StopCronometer();

                await sqlServerContext.DisposeAsync();
            }
        }
    }
}
