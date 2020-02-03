using Stroll.Dtos;
using Stroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Interfaces
{
    public interface IClientUserRepository : IBaseRepository<ClientUser>
    {
        public Task<ClientUser> CreateClientUser(UserForRegisterDto userInfo, Guid newUserID);
    }
}
