using SingleLog.Enums;
using SingleLog.Models;

namespace SingleLog.Interfaces
{
    public interface ISingletonLogger<T1> where T1 : BaseLogObject
    {
        Task<T1> CreateBaseLogAsync();
        Task<T1> GetBaseLogAsync();
        Task WriteLogAsync(LogTypes typeLog, T1 value);
        Task WriteLogAsync(T1 value);
    }
}