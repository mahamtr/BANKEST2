using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BANKEST2.Core.Entities;
using BANKEST2.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BANKEST2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositService _accountDepositService;

        public DepositController(IDepositService accountDepositService)
        {
            _accountDepositService = accountDepositService;
        }

        [HttpPost]
        [Authorize]
        public TransactionInfo DepositToAccount([FromBody]TransactionRequest request)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Claim userNameClaim = identity.FindFirst("UserName");
            return _accountDepositService.DepositToAccount(userNameClaim, request);
        }

    }
}