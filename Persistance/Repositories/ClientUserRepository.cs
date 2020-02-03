using Stroll.Data;
using Stroll.Dtos;
using Stroll.Interfaces;
using Stroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Services
{
    public class ClientUserRepository : BaseRepository<ClientUser>, IClientUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientUser> CreateClientUser(UserForRegisterDto userInfo, Guid newUserID)
        {
            var newClientUser = new ClientUser()
            {
                UserId = newUserID,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Phone = userInfo.Phone
            };

            await AddAsync(newClientUser);

            return newClientUser;
        }
    }
}
