using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Observer.Data.Interfaces;
using System.Data;

namespace Observer.Data.Context
{
    /// <summary>
    /// Context class for connection with SQL Server.
    /// </summary>
    public class SqlServerContext : DbContext, ISqlServerContext
    {
        private readonly string _connectionString;
        private SqlConnection _connection = null!;

        /// <summary>
        /// Context SQL Server constructor.
        /// </summary>
        /// <param name="connectionString">ConnectionString to close connection with database</param>
        public SqlServerContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer")!;
        }

        /// <summary>
        /// Close and dispose connection after use.
        /// </summary>
        public override async ValueTask DisposeAsync()
        {
            if (_connection is not null)
            {
                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.DisposeAsync();
            }
        }

        /// <summary>
        /// Retrieve opened connection with SQL Server.
        /// </summary>
        /// <returns>Opened connection on IDbConnection.</returns>
        public async Task<SqlConnection> GetConnection()
        {
            try
            {
                if (_connection is null || _connection.State.Equals(ConnectionState.Closed))
                {
                    _connection = new SqlConnection(_connectionString);
                }

                await _connection.OpenAsync();

                return _connection;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}