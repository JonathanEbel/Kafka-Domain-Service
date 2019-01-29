using Organizations.Commands;
using System;

namespace Organizations.Domain.CommandHandlers
{
    public interface IAddOrganizationCommandHandler
    {
        Guid HandleCommand(AddOrganizationCommand cmd);
    }
}
