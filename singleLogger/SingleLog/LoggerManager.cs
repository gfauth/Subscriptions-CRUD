using Newtonsoft.Json;
using NLog;
using SingleLog.Enums;
using SingleLog.Utils;

namespace SingleLog
{
    public sealed class LoggerManager : IDisposable
    {
        private readonly Logger messageLog;

        public LoggerManager()
        {
            messageLog = LogManager.GetCurrentClassLogger();
        }

        public void Dispose()
        {
            LogManager.Shutdown();
        }

        public void WriteLog(dynamic message)
        {
            WriteLog(message.Level, message);
        }

        public void WriteLog(LogTypes type, object obj)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonObject = Strings.Clean(JsonConvert.SerializeObject(obj, jsonSerializerSettings));

            switch (type)
            {
                case LogTypes.INFO:
                    messageLog.Info(jsonObject);
                    break;
                case LogTypes.WARN:
                    messageLog.Warn(jsonObject);
                    break;
                case LogTypes.ERROR:
                    messageLog.Error(jsonObject);
                    break;
                case LogTypes.FATAL:
                default:
                    messageLog.Fatal(jsonObject);
                    break;
            }
        }
    }
}