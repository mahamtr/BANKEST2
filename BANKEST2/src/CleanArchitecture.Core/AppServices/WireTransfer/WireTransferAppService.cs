using BANKEST2.Core.Entities;
using BANKEST2.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BANKEST2.Core.AppServices.WireTransfer
{
    public class WireTransferAppService : IWireTransferAppService
    {
        private IUnitOfWork _unitOfWork;
        private IAccountMapper _accountMapper;

        public WireTransferAppService(IUnitOfWork unitOfWork,
                                  IAccountMapper accountMapper)
        {
            _unitOfWork = unitOfWork;
            _accountMapper = accountMapper;
        }

        public TransactionInfo WireTransferToAccount(Claim claim, TransactionRequest request)
        {
            if (request.Amount <= 0) throw new NullReferenceException("Amount to transfer must be greater than 0");
            Users accountUser = _accountMapper.GetAccountNumberByUserName(claim);
            var accounts = _unitOfWork.Accounts
                .GetFiltered(i => i.AccountId == accountUser.Account || i.AccountId == request.AccountToTransfer);
            var accountToRemote = accounts.FirstOrDefault(i => i.AccountId == accountUser.Account);
            var accountToAdd = accounts.FirstOrDefault(i => i.AccountId == request.AccountToTransfer);
            if (accountToAdd == null)
                return new TransactionInfo
                {
                    TransactionException = new Exception("Account to Transfer does not exists")
                };
            accountToRemote.Balance -= request.Amount;
            accountToAdd.Balance += request.Amount;

            var transactionLog = new TransactionLog
            {
                DestinationAccount = accountToAdd.AccountId,
                SourceAccount = accountToRemote.AccountId,
                TransactionType = TransactionTypes.WireTransfer,
                UserName = accountUser.UserName,
                Date = DateTime.Now,
                Amount = request.Amount
            };
            _unitOfWork.TransactionLog.Add(transactionLog);

            _unitOfWork.Commit();
            return new TransactionInfo { 
                PrimaryAccount= accountToRemote.AccountId,
                PrimaryAccountBalance = accountToRemote.Balance,
                SecondaryAccount = accountToAdd.AccountId,
                TransactionType = TransactionTypes.WireTransfer
            };
        }
    }
}
