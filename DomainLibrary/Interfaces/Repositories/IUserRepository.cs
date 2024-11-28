using DomainLibrary.Entities;
using DomainLibrary.Envelopes;

namespace DomainLibrary.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IResponse<Users>> InsertUser(Users userData);
        Task<IResponse<Users>> SelectUser(int userId);
        Task<IResponse<bool>> UpdateUser(Users userData);
        Task<IResponse<bool>> DeleteUser(int userId);
    }
}
