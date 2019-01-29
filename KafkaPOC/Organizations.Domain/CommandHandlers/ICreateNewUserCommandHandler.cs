using Organizations.Commands;
using System;
using System.Threading.Tasks;

namespace Organizations.Domain.CommandHandlers
{
    public interface ICreateNewUserCommandHandler
    {
        Task<Guid> HandleCommand(CreateNewUserCommand cmd);
    }
}
