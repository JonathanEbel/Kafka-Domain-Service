using Core.Constants;
using Identity.Commands;
using Identity.Domain.Repos;
using Identity.Models;
using System;

namespace Identity.Domain.CommandHandlers.Implementations
{
    public class CreateNewApplicationUserCommandHandler : ICreateNewApplicationUserCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public CreateNewApplicationUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public ApplicationUser Handle(CreateNewApplicationUserCommand cmd, bool useStrongPassword)
        {
            if (cmd.CommandId == null)
                cmd.CommandId = Guid.NewGuid();

            var applicationUser = new ApplicationUser(cmd.UserName, cmd.Password, cmd.ConfirmPassword, useStrongPassword, true);

            foreach (var role in cmd.Roles)
                applicationUser.AddClaim(Constants.CLAIM_ROLE, role);

            _applicationUserRepository.Add(applicationUser);
            _applicationUserRepository.Save();

            //fire event here...

            return applicationUser;
        }

    }
}
