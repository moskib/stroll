using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IAuthRepository Auth { get; }
        Task<int> SaveChangesAsync();
    }
}
