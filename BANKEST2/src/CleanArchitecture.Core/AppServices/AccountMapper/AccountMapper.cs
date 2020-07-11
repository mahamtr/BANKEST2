using BANKEST2.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace BANKEST2.Core.AppServices.AccountMapper
{
    public class AccountMapper : IAccountMapper
    {
        private IUnitOfWork _unitOfWork;

        public AccountMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Users GetAccountNumberByUserName(Claim claim)
        {
            var userName = claim.Value;
            return _unitOfWork.Users.GetFiltered(i => i.UserName == userName).FirstOrDefault();
        }
    }
}
