using System;
using System.Threading.Tasks;
using Stroll.Models;

namespace Stroll.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userEmail, string password);
        Task<bool> UserExists(string userEmail);
    }
}
