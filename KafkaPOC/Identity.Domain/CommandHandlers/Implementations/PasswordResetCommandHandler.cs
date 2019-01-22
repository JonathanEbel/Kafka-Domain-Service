using Identity.Commands;
using Identity.Domain.Repos;
using System;

namespace Identity.Domain.CommandHandlers.Implementations
{
    public class PasswordResetCommandHandler : IPasswordResetCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public PasswordResetCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public void Handle(PasswordResetCommand cmd, bool useStrongPassword)
        {
            if (cmd.CommandId == null)
                cmd.CommandId = Guid.NewGuid();

            var user = _applicationUserRepository.Get(cmd.ApplicationUserId);
            if (user != null)
            {
                user.UpdatePassword(cmd.NewPassword, cmd.NewPasswordConfirm, cmd.OldPassword, useStrongPassword);
                _applicationUserRepository.Save();

                //fire event here...
            }
        }

        public void Dispose()
        {
            _applicationUserRepository.Dispose();
        }
    }
}
