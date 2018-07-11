using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.API.Attributes;
using Starter.Common.DomainTaskStatus;
using Starter.Services.Token;
using Starter.Services.Token.Models;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly DomainTaskStatus _taskStatus;

        public AuthController(ITokenService tokenService, DomainTaskStatus taskStatus)
        {
            _tokenService = tokenService;
            _taskStatus = taskStatus;
        }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <param name="loginCredentials">Base login credentials</param>
        /// <returns><see cref="TokenModel"/></returns>
        [HttpPost]
        [Route("token")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [ProducesResponseType(typeof(TokenModel), 200)]
        public IActionResult AccessToken([FromBody] LoginCredentials loginCredentials)
        {
            var token = _tokenService.GetToken(loginCredentials);
            if (!_taskStatus.HasErrors)
            {
                return Ok(token);
            }
            return BadRequest(_taskStatus.ErrorCollection);
        }

        [HttpPost]
        [Route("refresh_token")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [ProducesResponseType(typeof(TokenModel), 200)]
        public IActionResult RefreshToken([FromBody] RefreshTokenModel refreshToken)
        {
            var token = _tokenService.GetRefreshToken(refreshToken);
            if (!_taskStatus.HasErrors)
            {
                return Ok(token);
            }
            return BadRequest(_taskStatus.ErrorCollection);
        }
    }
}