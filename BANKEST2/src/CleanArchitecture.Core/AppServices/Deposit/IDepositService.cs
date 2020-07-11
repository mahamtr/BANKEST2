using BANKEST2.Core.Entities;
using System.Security.Claims;

namespace BANKEST2.Core.Interfaces
{
    public interface IDepositService
    {
        TransactionInfo DepositToAccount( Claim userNameClaim,TransactionRequest request);
    }
}
