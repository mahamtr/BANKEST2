using BANKEST2.Core.Entities;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BANKEST2.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Users> Users { get; private set; }
        public IRepository<Accounts> Accounts { get; private set; }
        public IRepository<TestsHistory> TestsHistory{ get; private set; }

        public IRepository<TransactionLog> TransactionLog { get; private set; }

        private readonly BankDbContext _context;


        public UnitOfWork(BankDbContext context)
        {
            _context = context;
            Accounts = new Repository<Accounts>(context);
            Users = new Repository<Users>(context);
            TransactionLog = new Repository<TransactionLog>(context);
            TestsHistory = new Repository<TestsHistory>(context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
