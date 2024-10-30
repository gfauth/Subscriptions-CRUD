using SingleLog.Enums;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace SingleLog
{
    public sealed class SingletonLogger<T1> : ISingletonLogger<T1> where T1 : BaseLogObject
    {
        private readonly LoggerManager _loggerManager;
        private T1? _baseLog;

        public SingletonLogger()
        {
            if (_loggerManager is null)
                _loggerManager = new LoggerManager();
        }

        public Task<T1> CreateBaseLogAsync()
        {
            _baseLog = (T1)Activator.CreateInstance(typeof(T1))!;

            return Task.FromResult(_baseLog);
        }

        public Task<T1> GetBaseLogAsync() => Task.FromResult(_baseLog)!;

        public Task WriteLogAsync(LogTypes typeLog, T1 value)
        {
            long elapsedMilliseconds = 0;

            foreach (var step in value.Steps.Values)
            {
                if (step is not null && step is SubLog)
                    ((SubLog)step).StopCronometer();

                elapsedMilliseconds += ((SubLog)step!).ElapsedMilliseconds;
            }

            value.ElapsedMilliseconds = elapsedMilliseconds;

            _loggerManager.WriteLog(typeLog, value);

            return Task.CompletedTask;
        }

        public async Task WriteLogAsync(T1 value) => await WriteLogAsync(value.Level, value);
    }
}