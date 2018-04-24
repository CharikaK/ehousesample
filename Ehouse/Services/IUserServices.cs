using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehouse.Services
{
    public interface IUserServices
    {
        // method to take userame and password and outputs as a User. Return a task of bool
        Task<bool> ValidateCredentials(string username, string password, out User user);
    }

        //user entity
        public class User
        {
        
            public User(string Username)
            {
                Usernmame = Username;
            }

            public string Usernmame { get; }
        }
        
 }

