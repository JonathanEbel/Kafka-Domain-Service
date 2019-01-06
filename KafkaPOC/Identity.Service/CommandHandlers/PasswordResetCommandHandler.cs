using Identity.Commands;
using Identity.Domain.CommandHandlers;
using Identity.Domain.Repos;

namespace Identity.Service.CommandHandlers
{
    public class PasswordResetCommandHandler : IPasswordResetCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public PasswordResetCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public void Handle(PasswordResetCommand cmd)
        {
            var user = _applicationUserRepository.GetById(cmd.ApplicationUserId);
            if (user != null)
            {
                user.UpdatePassword(cmd.NewPassword, cmd.NewPasswordConfirm, cmd.OldPassword);
                _applicationUserRepository.Save();

                //fire event here...
            }
        }
    }
}
