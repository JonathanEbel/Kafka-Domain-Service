using Identity.Commands;
using Identity.Models;
using System;
using System.Threading.Tasks;

namespace Identity.Domain.CommandHandlers
{
    public interface ICreateNewApplicationUserCommandHandler : IDisposable
    {
        Task<ApplicationUser> Handle(CreateNewApplicationUserCommand cmd, bool useStrongPassword);
    }
}
