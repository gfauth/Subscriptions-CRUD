using Dapper;
using DomainLibrary.Envelopes;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Models.LogModels;
using DomainLibrary.Settings;
using Microsoft.Data.SqlClient;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace DatabaseLibrary
{
    /// <summary>
    /// Sealed class that will run database routines at application runtime
    /// </summary>
    /// <typeparam name="T">Represent a type of object will return into IResponse</typeparam>
    public sealed class QueryRunner<T>
    {
        /// <summary>
        /// Execute a query that will return a single object or single response.
        /// </summary>
        /// <param name="singleLog">ISingletonLogger object.</param>
        /// <param name="logStep">log step string.</param>
        /// <param name="sqlServerContext">ISqlServerContext context.</param>
        /// <param name="query">string query.</param>
        /// <param name="parameters">representation of parameter, object ir a struct data.</param>
        /// <returns>IResponse</returns>
        public static async Task<IResponse<T>> ExecuteQuerySingleTAsync(ISingletonLogger<LogModel> singleLog, string logStep,
            ISqlServerContext sqlServerContext, string query, object? parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLogDatabase();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StartCronometer();

            sublog.query = query;
            sublog.parameters = parameters;

            try
            {
                var dynamicParameters = new DynamicParameters(parameters);

                return await Policies.GetPolicyAsync().ExecuteAsync(async Task<IResponse<T>> () =>
                {
                    await using SqlConnection connection = await sqlServerContext.GetConnection();

                    var response = await connection.QuerySingleAsync<T>(query, dynamicParameters);

                    return new ResponseOk<T>(response);
                });
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// Execute a query that will return a list of objects.
        /// </summary>
        /// <param name="singleLog">ISingletonLogger object.</param>
        /// <param name="logStep">log step string.</param>
        /// <param name="sqlServerContext">ISqlServerContext context.</param>
        /// <param name="query">string query.</param>
        /// <param name="parameters">representation of parameter, object ir a struct data.</param>
        /// <returns>IResponse with a list</returns>
        public static async Task<IResponse<List<T>>> ExecuteQueryListTAsync(ISingletonLogger<LogModel> singleLog, string logStep,
            ISqlServerContext sqlServerContext, string query, object? parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StartCronometer();

            try
            {
                var dynamicParameters = new DynamicParameters(parameters);

                return await Policies.GetPolicyAsync().ExecuteAsync(async Task<IResponse<List<T>>> () =>
                {
                    await using SqlConnection connection = await sqlServerContext.GetConnection();

                    var response = await connection.QueryAsync<T>(query, dynamicParameters);

                    return new ResponseOk<List<T>>(response.ToList());

                });
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