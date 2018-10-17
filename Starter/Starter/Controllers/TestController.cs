using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Services.Test;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _testService.TestAll();
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetManagerById(int id)
        {
            return Ok(_testService.GetManagerById(id));
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllManagers()
        {
            return Ok(_testService.GetAllManagers());
        }
    }
}