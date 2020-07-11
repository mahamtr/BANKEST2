using BANKEST2.Core.Entities;
using BANKEST2.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BANKEST2.Core.AppServices.WithDrawalService
{
    public class WithDrawalAppService : IWithDrawalAppService
    {
        private IUnitOfWork _unitOfWork;
        private IAccountMapper _accountMapper;

        public WithDrawalAppService(IUnitOfWork unitOfWork,
                                  IAccountMapper accountMapper)
        {
            _unitOfWork = unitOfWork;
            _accountMapper = accountMapper;
        }
        public TransactionInfo WidthDrawalFromAccount(Claim claim, TransactionRequest request)
        {
            if (request.Amount <= 0) throw new Exception("Amount to withdraw cannot 0 or negative");
            Users accountUser = _accountMapper.GetAccountNumberByUserName(claim);
            var accountToWithDraw = _unitOfWork.Accounts
                .GetFiltered(i => i.AccountId == accountUser.Account).FirstOrDefault();
            if (request.Amount > accountToWithDraw.Balance) throw new Exception("Amount to withdraw is greater that account balance");
            accountToWithDraw.Balance -= request.Amount;

            var transactionLog = new TransactionLog
            {
                DestinationAccount = accountToWithDraw.AccountId,
                TransactionType = TransactionTypes.WithDrawal,
                UserName = accountUser.UserName,
                Date = DateTime.Now,
                Amount = request.Amount
            };
            _unitOfWork.TransactionLog.Add(transactionLog);
            _unitOfWork.Commit();
            return new TransactionInfo
            {
                PrimaryAccount = accountToWithDraw.AccountId,
                PrimaryAccountBalance = accountToWithDraw.Balance,
                TransactionType = TransactionTypes.WithDrawal
            };
        }
    }
}
