using BANKEST2.Core.Entities;
using BANKEST2.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BANKEST2.Core.Services.AccountQuery
{
    public class QueryService : IQueryService
    {
        private IUnitOfWork _unitOfWork;
        private IAccountMapper _accountMapper;

        public QueryService(IUnitOfWork unitOfWork,
                                  IAccountMapper accountMapper)
        {
            _unitOfWork = unitOfWork;
            _accountMapper = accountMapper;
        }

        public TransactionInfo GetAccountBalance(Claim claim)
        {
            Users accountUser = _accountMapper.GetAccountNumberByUserName(claim);
            var account = _unitOfWork.Accounts.GetFiltered(i => i.AccountId == accountUser.Account).FirstOrDefault();
            var transactionLog = new TransactionLog
            {
                DestinationAccount = account.AccountId,
                TransactionType = TransactionTypes.Query,
                UserName = accountUser.UserName,
                Date = DateTime.Now,
            };
            _unitOfWork.TransactionLog.Add(transactionLog);
            _unitOfWork.Commit();
            return new TransactionInfo
            {
                PrimaryAccount = account.AccountId,
                PrimaryAccountBalance = account.Balance,
                TransactionType = TransactionTypes.Query
            };
        }

       
    }
}
