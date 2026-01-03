using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ErrorHandling;
using Auth_User.Services;
using Auth_User.Params.Inputs;
using Auth_User.API.Controllers;
using Common.Helper.HttpResultHelper;

namespace Auth_User.API.Controllers
{
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ITokenService TokenService;
        private readonly ILogger<TokenController> Logger;
        private readonly IHttpResultHelper _httpResultHelper;

        public TokenController(ITokenService tokenService, ILogger<TokenController> logger, IHttpResultHelper httpResultHelper)
        {
            this.TokenService = tokenService;
            Logger = logger;
            _httpResultHelper = httpResultHelper;
        }
        
        [HttpPost("ClientLogin")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> Login([FromBody]ClientLoginParam input)
        {
            try
            {
                var result = await TokenService.ClientLoginAsync(input);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> Login([FromBody]LoginParam input)
        { 
            try
            {
                var result = await TokenService.LoginAsync(input);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("LoginFromAuth")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> LoginFromAuth([FromQuery] string UserGuId, [FromQuery] string EmployeeNo = null)
        {
            try
            {
                var result = await TokenService.LoginFromAuthByToken(UserGuId, EmployeeNo);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody]LogoutParam input)
        {
            var userID = HttpContext.User.Claims.Where(x => x.Type == "userid").SingleOrDefault();
            await TokenService.LogoutAsync(input);
            return Ok();
        }

        //Task<JsonWebToken> LoginFromAuthByToken(string GuID, string EmployeeNo = null);

        //Task<GenerateUserTokenResult> GenerateUserToken(Guid UserGUID);

        [HttpGet("LoginFromAuthByToken")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> LoginFromAuthByToken([FromQuery] string UserGuId, [FromQuery] string EmployeeNo = null)
        {
            try
            {
                var result = await TokenService.LoginFromAuthByToken(UserGuId, EmployeeNo);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GenerateUserToken")]
        [ProducesResponseType(200, Type = typeof(JsonWebToken))]
        public async Task<IActionResult> GenerateUserToken([FromQuery] Guid UserGuId)
        {
            try
            {
                var result = await TokenService.GenerateUserToken(UserGuId);
                return Ok(result);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
