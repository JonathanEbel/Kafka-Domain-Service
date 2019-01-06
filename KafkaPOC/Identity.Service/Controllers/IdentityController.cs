using System;
using Identity.Commands;
using Identity.Domain.CommandHandlers;
using Identity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IPasswordResetCommandHandler _passwordResetCommandHandler;
        private readonly ICreateNewApplicationUserCommandHandler _createNewApplicationUserCommandHandler;

        public IdentityController(IPasswordResetCommandHandler passwordResetCommandHandler, 
                    ICreateNewApplicationUserCommandHandler createNewApplicationUserCommandHandler)
        {
            _passwordResetCommandHandler = passwordResetCommandHandler;
            _createNewApplicationUserCommandHandler = createNewApplicationUserCommandHandler;
        }

        [HttpPatch]
        public IActionResult UpdatePassword([FromBody]PasswordResetCommand cmd)
        {
            try
            {
                _passwordResetCommandHandler.Handle(cmd);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Produces(typeof(NewUserCreatedResponseDto))]
        public IActionResult CreateNewUser([FromBody]CreateNewApplicationUserCommand cmd)
        {
            try
            {
                var applicationUser = _createNewApplicationUserCommandHandler.Handle(cmd);
                return Ok(new NewUserCreatedResponseDto
                {
                    Active = applicationUser.Active,
                    ApplicationUserId = applicationUser.Id
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
