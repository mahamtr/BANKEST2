using BANKEST2.Core.Entities;
using CleanArchitecture.Core.Entities;
using System;
using System.Text;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Users> Users { get; }
        IRepository<Accounts> Accounts{ get; }
        IRepository<TransactionLog> TransactionLog { get; }
        IRepository<TestsHistory> TestsHistory{ get; }


        int Commit();

    }
}
