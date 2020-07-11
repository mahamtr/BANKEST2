using BANKEST2.Core.AppServices.WithDrawalService;
using BANKEST2.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace BANKEST2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithDrawalController : ControllerBase
    {
        private  readonly IWithDrawalAppService _withDrawalAppService;

        public WithDrawalController(IWithDrawalAppService withDrawalAppService)
        {
            _withDrawalAppService = withDrawalAppService;
        }

        [HttpPost]
        [Authorize]
        public TransactionInfo WithDrawalFromAcount([FromBody]TransactionRequest request)
        {
           ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            Claim userNameClaim = identity.FindFirst("UserName");
            return _withDrawalAppService.WidthDrawalFromAccount(userNameClaim,request);
        }
    }
}