using System;
using System.Threading.Tasks;
using Identity.Commands;
using Identity.Common;
using Identity.Domain.CommandHandlers;
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
        private readonly AppSettingsSingleton _appSettings;

        public IdentityController(IPasswordResetCommandHandler passwordResetCommandHandler, 
                    ICreateNewApplicationUserCommandHandler createNewApplicationUserCommandHandler,
                    IOptions<AppSettingsSingleton> appSettings)
        {
            _passwordResetCommandHandler = passwordResetCommandHandler;
            _createNewApplicationUserCommandHandler = createNewApplicationUserCommandHandler;
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
    }
}
