using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Stroll.Data;
using System.Linq;
using Stroll.Interfaces;
using Stroll.Models;

namespace Stroll.Services
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> Login(string userEmail, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(el => el.Email == userEmail);

            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePassowrdHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);

            return user;
        }

        private void CreatePassowrdHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userEmail)
        {
            return await _context.Users.AnyAsync(user => user.Email == userEmail);
        }
    }
}
