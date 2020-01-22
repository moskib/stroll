using Stroll.Dtos;
using Stroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Interfaces
{
    public interface IBusinessUserRepository: IRepository<BusinessUser>
    {
        public Task<BusinessUser> CreateBusinessUser(UserForRegisterDto userInfo, Guid newUserID);
    }
}
