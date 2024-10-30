using SingleLog.Models;

namespace Observer.Presentation.Logs
{
    public class SubLogDatabase: SubLog
    {
        public int tryCount { get; set; }
        public string query { get; set; }
        public object? parameters { get; set; }
    }
}
