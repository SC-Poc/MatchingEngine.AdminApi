using System;
using System.Threading.Tasks;

namespace MatchingEngine.AdminApi.Services
{
    public interface IUsersService
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByCredentialsAsync(string email, string password);
    }
}
