using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BANKEST2.Core.Interfaces
{
    public interface IAccountMapper
    {
        public Users GetAccountNumberByUserName(Claim claim);
    }
}
