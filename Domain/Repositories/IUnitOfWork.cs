using System;
using System.Threading.Tasks;

namespace Stroll.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository Auth { get; }
        IBusinessUserRepository BusinessUser { get; }
        IClientUserRepository ClientUser { get; }
        Task<int> SaveChangesAsync();
    }
}
