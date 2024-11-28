using SingleLog.Models;

namespace DomainLibrary.Models.LogModels
{
    public class SubLogDatabase : SubLog
    {
        public int tryCount { get; set; }
        public string query { get; set; }
        public object? parameters { get; set; }
    }
}