using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;

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
