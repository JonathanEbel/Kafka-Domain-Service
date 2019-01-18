using Identity.Commands;
using Identity.Models;
using System.Threading.Tasks;

namespace Identity.Domain.CommandHandlers
{
    public interface ICreateNewApplicationUserCommandHandler
    {
        Task<ApplicationUser> Handle(CreateNewApplicationUserCommand cmd, bool useStrongPassword);
    }
}
