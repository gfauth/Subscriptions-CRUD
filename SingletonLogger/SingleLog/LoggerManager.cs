using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using NLog;
using SingleLog.Enums;
using SingleLog.Utils;

namespace SingleLog
{
    [ExcludeFromCodeCoverage]
    public sealed class LoggerManager : IDisposable
    {
        private readonly Logger message;

        public LoggerManager()
        {
            message = LogManager.GetCurrentClassLogger();
        }

        public void WriteLog(LogTypes type, object obj)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonObject = Strings.Clean(JsonConvert.SerializeObject(obj, jsonSerializerSettings));

            switch (type)
            {
                case LogTypes.INFO:
                    message.Info(jsonObject);
                    break;
                case LogTypes.WARN:
                    message.Warn(jsonObject);
                    break;
                case LogTypes.ERROR:
                    message.Error(jsonObject);
                    break;
                case LogTypes.FATAL:
                default:
                    message.Fatal(jsonObject);
                    break;
            }
        }

        public void Dispose() => LogManager.Shutdown();
    }
}