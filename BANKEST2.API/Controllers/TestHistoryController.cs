using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BANKEST2.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BANKEST2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestHistoryController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;

        public TestHistoryController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        [AllowAnonymous]
        [HttpGet]
        public List<TestsHistory> getTestHistory()
        {
            return _unitOfWork.TestsHistory.GetAll().OrderByDescending(i => i.Id).ToList();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("save")]
        public int AddTestHistory([FromBody]TestsHistory newTest)
        {
             _unitOfWork.TestsHistory.Add(newTest);
            _unitOfWork.Commit();
            return 1;
        }





    }
}