using Microsoft.AspNetCore.Mvc;
using Organizations.Commands;
using Organizations.Domain.CommandHandlers;
using Organizations.Dtos;
using System;
using System.Threading.Tasks;

namespace Organizations.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ICreateNewUserCommandHandler _createNewUserCommandHandler;

        public UserController(ICreateNewUserCommandHandler createNewUserCommandHandler)
        {
            _createNewUserCommandHandler = createNewUserCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateNewUserCommand cmd)
        {
            try
            {
                var userId = await _createNewUserCommandHandler.HandleCommand(cmd);
                return Ok(new NewUserResultDto { Id = userId });
            }
            catch(Exception ex)
            {
                if (ex is FormatException)
                    return BadRequest(ex.Message);
                //TODO: log what happened here...
                return BadRequest("There was an error");
            }
        }
    }
}
