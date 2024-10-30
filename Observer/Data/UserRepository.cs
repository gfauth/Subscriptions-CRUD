using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;

namespace Observer.Data
{
    /// <summary>
    /// Repository for entity Users.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// Repository constructor.
        /// </summary>
        /// <param name="sqlServerContext">Object ISqlServerContext.</param>
        /// <param name="singleLog">Object ISingleLog.</param>
        public UserRepository(ISqlServerContext sqlServerContext, ISingletonLogger<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        public async Task<IResponse<Users>> InsertUser(Users userData) =>
            await QueryRunner<Users>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.USER_DATABASE_CREATE_DATA, 
            _sqlServerContext, QueryData.InsertUsers, userData);

        public async Task<IResponse<Users>> SelectUser(int userId) =>
            await QueryRunner<Users>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.USER_DATABASE_RETRIEVE_DATA, 
            _sqlServerContext, QueryData.SelectOneUsers, userId);

        public async Task<IResponse<bool>> UpdateUser(Users userData) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.USER_DATABASE_UPDATE_DATA, 
            _sqlServerContext, QueryData.UpdateUsers, userData);

        public async Task<IResponse<bool>> DeleteUser(int userId) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.USER_DATABASE_DELETE_DATA, 
            _sqlServerContext, QueryData.DeleteUsers, userId);
    }
}
