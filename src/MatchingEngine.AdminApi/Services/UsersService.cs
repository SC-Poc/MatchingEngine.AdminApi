using System;
using System.Linq;
using System.Threading.Tasks;
using MatchingEngine.AdminApi.Configuration;

namespace MatchingEngine.AdminApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppConfig _configuration;

        public UsersService(AppConfig configuration)
        {
            _configuration = configuration;
        }

        public Task<User> GetByIdAsync(Guid userId)
        {
            User user = null;

            if (_configuration.AdminApi.Users != null)
            {
                user = _configuration.AdminApi.Users
                    .FirstOrDefault(o => o.Id == userId);
            }

            return Task.FromResult(user);
        }

        public Task<User> GetByCredentialsAsync(string email, string password)
        {
            User user = null;

            if (_configuration.AdminApi.Users != null)
            {
                user = _configuration.AdminApi.Users
                    .FirstOrDefault(o => o.Email == email && o.Password == password);
            }

            return Task.FromResult(user);
        }
    }
}
