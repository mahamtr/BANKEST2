using BANKEST2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BANKEST2.Core.AppServices.WireTransfer
{
    public interface IWireTransferAppService
    {
        public TransactionInfo WireTransferToAccount(Claim claim, TransactionRequest request);
    }
}
