using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehouse.Services
{
    public class DummyUserServices : IUserServices
    {
        //tuple
        private IDictionary<string, (string PasswordHash, User User)> _users =
            new Dictionary<string, (string PasswordHash, User User)>();
        // to make itpre-populate with some users, create IDictionary to get username and password
        public DummyUserServices(IDictionary<string, string> users)
        {
            foreach(var user in users)
            {
                // Has the password with NuGet Package
                _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));

            }
        }
        // after hasshing the password it is easy to use in Task<>
        public Task<bool> ValidateCredentials(string username, string password, out User user)
        {
            user = null;
            var key = username.ToLower();
            if (_users.ContainsKey(key))
            {
                var hash = _users[key].PasswordHash;
                if (BCrypt.Net.BCrypt.Verify(password,hash))
                {
                    user = _users[key].User;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
