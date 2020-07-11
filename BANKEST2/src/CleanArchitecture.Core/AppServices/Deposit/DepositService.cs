using BANKEST2.Core.Entities;
using BANKEST2.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace BANKEST2.Core.AppServices.NewFolder
{
    public class DepositService : IDepositService
    {
        private IUnitOfWork _unitOfWork;
        private IAccountMapper _accountMapper;

        public DepositService(IUnitOfWork unitOfWork,
                                  IAccountMapper accountMapper)
        {
            _unitOfWork = unitOfWork;
            _accountMapper = accountMapper;
        }


        public TransactionInfo DepositToAccount(Claim claim, TransactionRequest request)
        {
            if (request.Amount <= 0) throw new NullReferenceException("Amount to deposit must be greater than 0");
            Users accountUser = _accountMapper.GetAccountNumberByUserName(claim);
            var account = _unitOfWork.Accounts.GetFiltered(i => i.AccountId == accountUser.Account).FirstOrDefault();
            account.Balance += request.Amount;



            var transactionLog = new TransactionLog
            {
                DestinationAccount = account.AccountId,
                TransactionType = TransactionTypes.Deposit,
                UserName = accountUser.UserName,
                Date = DateTime.Now,
                Amount = request.Amount
            };
            _unitOfWork.TransactionLog.Add(transactionLog);

            _unitOfWork.Commit();
            return new TransactionInfo
            {
                PrimaryAccount = account.AccountId,
                PrimaryAccountBalance = account.Balance,
                TransactionType = TransactionTypes.Deposit
            };

        }
    }
}
