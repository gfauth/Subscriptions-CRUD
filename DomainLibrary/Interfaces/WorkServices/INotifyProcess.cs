using DomainLibrary.Envelopes;
using DomainLibrary.Models.Responses;

namespace DomainLibrary.Interfaces.WorkServices
{
    public interface INotifyProcess
    {
        public Task<IResponse<bool>> Process();
    }
}
