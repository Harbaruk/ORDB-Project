using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Common.DomainTaskStatus;
using Starter.Services.UserManagement;
using Starter.Services.UserManagement.Models;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api/registration")]
    public class RegistrationController : AbstractController
    {
        private readonly IUserManagementService _registrationService;

        public RegistrationController(IUserManagementService registrationService, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Register([FromBody] RegistrationUserModel model)
        {
            _registrationService.RegisterUser(model);
            return Ok();
        }
    }
}