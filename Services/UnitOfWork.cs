using Stroll.Data;
using Stroll.Interfaces;
using Stroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Auth = new AuthRepository(_context);
            BusinessUser = new BusinessUserRepository(_context);
            ClientUser = new ClientUserRepository(_context);
        }

        public IRepository<User> Users { get; private set; }
        public IAuthRepository Auth { get; private set; }
        public IBusinessUserRepository BusinessUser { get; private set; }
        public IClientUserRepository ClientUser { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
