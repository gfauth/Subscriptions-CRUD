using System.Text.Json.Serialization;
using System.Diagnostics;

namespace SingleLog.Models
{
    public class SubLog : IDisposable
    {
        public DateTime ExecutedAt { get; set; }

        public int TraceId { get; set; }

        public long ElapsedMilliseconds { get; set; }

        [JsonIgnore]
        internal Stopwatch Stopwatch { get; set; }

        [JsonIgnore]
        public Exception? Exception { get; set; }

        public SubLog()
        {
            ExecutedAt = DateTime.Now;
            Stopwatch = new Stopwatch();

            StartCronometer();
        }

        public void StartCronometer()
        {
            if (Stopwatch.IsRunning)
                Stopwatch.Start();
        }

        public void StopCronometer()
        {
            Stopwatch.Stop();
            ElapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
        }

        public void Dispose()
        {
            Stopwatch.Stop();
        }
    }
}