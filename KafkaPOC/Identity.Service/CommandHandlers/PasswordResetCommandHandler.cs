using Identity.Commands;
using Identity.Domain.CommandHandlers;
using Identity.Domain.Repos;
using Microsoft.Extensions.Options;
using System;

namespace Identity.Service.CommandHandlers
{
    public class PasswordResetCommandHandler : IPasswordResetCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly AppSettingsSingleton _appSettings;

        public PasswordResetCommandHandler(IApplicationUserRepository applicationUserRepository,
                                    IOptions<AppSettingsSingleton> appSettings)
        {
            _applicationUserRepository = applicationUserRepository;
            _appSettings = appSettings.Value;
        }

        public void Handle(PasswordResetCommand cmd)
        {
            if (cmd.CommandId == null) 
                cmd.CommandId = Guid.NewGuid();

            var user = _applicationUserRepository.GetById(cmd.ApplicationUserId);
            if (user != null)
            {
                user.UpdatePassword(cmd.NewPassword, cmd.NewPasswordConfirm, cmd.OldPassword, _appSettings.UseStrongPassword);
                _applicationUserRepository.Save();

                //fire event here...
            }
        }
    }
}
