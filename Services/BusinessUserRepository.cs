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
    public class BusinessUserRepository : Repository<BusinessUser>, IBusinessUserRepository
    {
        public BusinessUserRepository(ApplicationDbContext context): base(context)
        {
        }

        public ApplicationDbContext context
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<BusinessUser> CreateBusinessUser(UserForRegisterDto userInfo, Guid newUserID)
        {

            var newBusinessUser = new BusinessUser()
            {
                UID = Guid.NewGuid(),
                UserID = newUserID,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Phone = userInfo.Phone,
            };

            await context.BusinessUsers.AddAsync(newBusinessUser);

            return newBusinessUser;
        }
    }
}
