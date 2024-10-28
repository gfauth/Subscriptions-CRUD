using Microsoft.Data.SqlClient;

namespace Observer.Data.Interfaces
{
    public interface ISqlServerContext : IDisposable
    {
        public Task<SqlConnection> GetConnection();

        public ValueTask DisposeAsync();
    }
}