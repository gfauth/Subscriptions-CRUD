﻿using Dapper;
using Microsoft.Data.SqlClient;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using Observer.Settings;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace Observer.Data
{
    /// <summary>
    /// Sealed class that will run database routines at application runtime
    /// </summary>
    /// <typeparam name="T">Represent a type of object will return into IResponse</typeparam>
    public sealed class QueryRunner<T1, T2>
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
        public static async Task<IResponse<T1>> ExecuteQuerySingleTAsync(ISingletonLogger<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, T2? parameters)
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

                return await Policies.GetPolicyAsync().ExecuteAsync(async Task<IResponse<T1>> () =>
                {
                    await using SqlConnection connection = await sqlServerContext.GetConnection();

                    var response = await connection.QuerySingleAsync<T1>(query, dynamicParameters);

                    return new ResponseOk<T1>(response);
                });
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;

                if (ex.Message.Equals("Sequence contains no elements"))
                    return new ResponseError<T1>("No data found.");

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
        public static async Task<IResponse<List<T1>>> ExecuteQueryListTAsync(ISingletonLogger<LogModel> singleLog, string logStep, ISqlServerContext sqlServerContext, string query, T2? parameters)
        {
            var baseLog = await singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(logStep, sublog);
            sublog.StartCronometer();

            try
            {
                var dynamicParameters = new DynamicParameters(parameters);

                return await Policies.GetPolicyAsync().ExecuteAsync(async Task<IResponse<List<T1>>> () =>
                {
                    await using SqlConnection connection = await sqlServerContext.GetConnection();

                    var response = await connection.QueryAsync<T1>(query, dynamicParameters);

                    return new ResponseOk<List<T1>>(response.ToList());

                });
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;

                if (ex.Message.Equals("Sequence contains no elements"))
                    return new ResponseError<List<T1>>("No data found.");

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