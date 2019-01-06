using Identity.Commands;
using Identity.Models;

namespace Identity.Domain.CommandHandlers
{
    public interface ICreateNewApplicationUserCommandHandler
    {
        ApplicationUser Handle(CreateNewApplicationUserCommand cmd);
    }
}
