﻿using Organizations.Commands;
using System;
using System.Threading.Tasks;

namespace Organizations.Domain.CommandHandlers
{
    public interface IAddOrganizationCommandHandler
    {
        Task<Guid> HandleCommand(AddOrganizationCommand cmd);
    }
}
