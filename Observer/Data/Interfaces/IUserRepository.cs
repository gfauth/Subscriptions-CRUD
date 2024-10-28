using Observer.Data.Entities;
using Observer.Domain.ResponsesEnvelope;

namespace Observer.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IResponse<Users>> InsertUser(Users userData);
        Task<IResponse<Users>> SelectUser(int userId);
        Task<IResponse<bool>> UpdateUser(Users userData);
        Task<IResponse<bool>> DeleteUser(int userId);
    }
}
