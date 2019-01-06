using Core.Constants;
using Identity.Commands;
using Identity.Domain.CommandHandlers;
using Identity.Domain.Repos;
using Identity.Models;
using Microsoft.Extensions.Options;
using System;

namespace Identity.Service.CommandHandlers
{
    public class CreateNewApplicationUserCommandHandler : ICreateNewApplicationUserCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly AppSettingsSingleton _appSettings;

        public CreateNewApplicationUserCommandHandler(IApplicationUserRepository applicationUserRepository, IOptions<AppSettingsSingleton> appSettings)
        {
            _applicationUserRepository = applicationUserRepository;
            _appSettings = appSettings.Value;
        }

        public ApplicationUser Handle(CreateNewApplicationUserCommand cmd)
        {
            if (cmd.CommandId == null)
                cmd.CommandId = Guid.NewGuid();

            var applicationUser = new ApplicationUser(cmd.UserName, cmd.Password, cmd.ConfirmPassword, _appSettings.UseStrongPassword, true);

            foreach (var role in cmd.Roles)
                applicationUser.AddClaim(Constants.CLAIM_ROLE, role);

            _applicationUserRepository.Add(applicationUser);
            _applicationUserRepository.Save();

            //fire event here...

            return applicationUser;
        }
    }
}
