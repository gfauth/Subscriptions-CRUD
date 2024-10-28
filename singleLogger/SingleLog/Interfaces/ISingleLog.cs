using SingleLog.Enums;
using SingleLog.Models;

namespace SingleLog.Interfaces
{
    public interface ISingleLog<T1> where T1 : BaseLogStep
    {
        Task<T1> CreateBaseLogAsync();
        Task<T1> GetBaseLogAsync();
        Task WriteLogAsync(LogTypes typeLog, T1 value);
        Task WriteLogAsync(T1 value);
    }
}