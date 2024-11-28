using DomainLibrary.Envelopes;
using DomainLibrary.Models.Requests;
using DomainLibrary.Models.Responses;

namespace DomainLibrary.Interfaces.Services
{
    public interface IUserServices
    {
        public Task<IResponse<ResponseEnvelope>> RetrieveUser(int userId);
        public Task<IResponse<ResponseEnvelope>> CreateUser(UserRequest user);
        public Task<IResponse<ResponseEnvelope>> UpdateUser(int userId, UserRequest user);
        public Task<IResponse<ResponseEnvelope>> DeleteUser(int userId);
    }
}
