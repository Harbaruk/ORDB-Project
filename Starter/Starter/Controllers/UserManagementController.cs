using Microsoft.AspNetCore.Mvc;
using Starter.Services.ADOServices.UserManagement;
using Starter.Services.UserManagement;
using Starter.Services.UserManagement.Models;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api/user_management")]
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IADOUserManagement _iADOUserManagement;

        public UserManagementController(IUserManagementService userManagementService, IADOUserManagement iADOUserManagement)
        {
            _userManagementService = userManagementService;
            _iADOUserManagement = iADOUserManagement;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult Register([FromBody] RegistrationUserModel registrationModel)
        {
            _userManagementService.RegisterUser(registrationModel);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_iADOUserManagement.GetById(id));
        }
    }
}