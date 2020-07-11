using BANKEST2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BANKEST2.Core.AppServices.WithDrawalService
{
    public interface IWithDrawalAppService
    {
        TransactionInfo WidthDrawalFromAccount(Claim claim,TransactionRequest   request);
    }
}
