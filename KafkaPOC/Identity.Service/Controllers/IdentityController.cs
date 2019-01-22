using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Commands;
using Identity.Common;
using Identity.Domain.CommandHandlers;
using Identity.Domain.Repos;
using Identity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IPasswordResetCommandHandler _passwordResetCommandHandler;
        private readonly ICreateNewApplicationUserCommandHandler _createNewApplicationUserCommandHandler;
        private readonly ILoginCommandHandler _loginCommandHandler;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly AppSettingsSingleton _appSettings;

        public IdentityController(IPasswordResetCommandHandler passwordResetCommandHandler, 
                    ICreateNewApplicationUserCommandHandler createNewApplicationUserCommandHandler,
                    IOptions<AppSettingsSingleton> appSettings, ILoginCommandHandler loginCommandHandler,
                    IApplicationUserRepository applicationUserRepository)
        {
            _passwordResetCommandHandler = passwordResetCommandHandler;
            _createNewApplicationUserCommandHandler = createNewApplicationUserCommandHandler;
            _loginCommandHandler = loginCommandHandler;
            _applicationUserRepository = applicationUserRepository;
            _appSettings = appSettings.Value;
        }

        [HttpPatch]
        public IActionResult UpdatePassword([FromBody]PasswordResetCommand cmd)
        {
            try
            {
                _passwordResetCommandHandler.Handle(cmd, _appSettings.UseStrongPassword);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Produces(typeof(NewUserCreatedResponseDto))]
        public async Task<IActionResult> CreateNewUser([FromBody]CreateNewApplicationUserCommand cmd)
        {
            try
            {
                var applicationUser = await _createNewApplicationUserCommandHandler.Handle(cmd, _appSettings.UseStrongPassword);
                return Ok(new NewUserCreatedResponseDto
                {
                    Active = applicationUser.Active,
                    ApplicationUserId = applicationUser.Id
                });
            }
            catch(Exception ex)
            {
                if (ex is DbUpdateException)
                    return BadRequest("That username is already in use.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Produces(typeof(IdentityLoginResponseDto))]
        public async Task<IActionResult> Login([FromBody]LoginCommand cmd)
        {
            var authResponse = await _loginCommandHandler.HandleCommand(cmd);

            if (!authResponse.LoginSuccess)
                return StatusCode(401, authResponse.FailureReason);

            var user = _applicationUserRepository.GetByIdWithClaims(authResponse.IdentityUserId);
            var result = new IdentityLoginResponseDto
            {
                IdentityUserId = user.Id,
                ExpirationInMinutes = _appSettings.tokenExpirationInMinutes,
                TokenSubject = user.UserName,
                Claims = new List<IdentityClaimDto>()
            };
            result.Claims.AddRange(user.Claims.Select(x => new IdentityClaimDto { ClaimKey = x.claimKey, ClaimValue = x.claimValue }).ToList());

            return Ok(result); 
        }
    }
}
