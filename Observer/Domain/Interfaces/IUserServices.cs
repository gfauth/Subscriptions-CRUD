using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using Observer.Domain.ResponsesEnvelope;

namespace Observer.Domain.Interfaces
{
    public interface IUserServices
    {
        public Task<IResponse<ResponseEnvelope>> RetrieveUser(int userId);
        public Task<IResponse<ResponseEnvelope>> CreateUser(UserRequest user);
        public Task<IResponse<ResponseEnvelope>> UpdateUser(int userId, UserRequest user);
        public Task<IResponse<ResponseEnvelope>> DeleteUser(int userId);
    }
}
