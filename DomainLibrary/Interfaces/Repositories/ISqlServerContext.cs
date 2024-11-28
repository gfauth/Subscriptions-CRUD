using Microsoft.Data.SqlClient;

namespace DomainLibrary.Interfaces.Repositories
{
    public interface ISqlServerContext : IDisposable
    {
        public Task<SqlConnection> GetConnection();

        public ValueTask DisposeAsync();
    }
}