using SingleLog.Models;

namespace Observer.Domain.Models.LogModels
{
    public class SubLogDatabase : SubLog
    {
        public int tryCount { get; set; }
        public string query { get; set; }
        public object? parameters { get; set; }
    }
}