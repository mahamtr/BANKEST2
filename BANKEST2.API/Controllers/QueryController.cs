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
    public class QueryController : ControllerBase
    {
        private IQueryService _accountQueryService;
        public QueryController(IQueryService accountQueryService)
        {
            _accountQueryService = accountQueryService;
        }

        [HttpGet]
        [Authorize]
        public TransactionInfo GetAccountBalance()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Claim userNameClaim = identity.FindFirst("UserName");
            return _accountQueryService.GetAccountBalance(userNameClaim);
        }
    }
}