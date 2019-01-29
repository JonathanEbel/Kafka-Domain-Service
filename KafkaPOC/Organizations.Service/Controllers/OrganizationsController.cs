using System;
using System.Threading.Tasks;
using Core.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Organizations.Commands;
using Organizations.Domain.CommandHandlers;
using Organizations.Dtos;

namespace Organizations.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IAddOrganizationCommandHandler _addOrganizationCommandHandler;

        public OrganizationsController(IAddOrganizationCommandHandler addOrganizationCommandHandler)
        {
            _addOrganizationCommandHandler = addOrganizationCommandHandler;
        }

        [HttpPost]
        [Produces(typeof(NewOrgResultDto))]
        public async Task<IActionResult> CreateOrg([FromBody] AddOrganizationCommand cmd)
        {
            try
            {
                var newOrgId = await _addOrganizationCommandHandler.HandleCommand(cmd);
                return Ok(new NewOrgResultDto { OrganizationID = (Guid)newOrgId });
            }
            catch(Exception ex)
            {
                if (ex is FormatException || ex is UnverifiedOrganizationException)
                    return BadRequest(ex.Message);
                //TODO: log what happened here...
                return BadRequest("There was an error.");
            }
            
        }

    }
}
