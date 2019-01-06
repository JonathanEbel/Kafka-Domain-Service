using Identity.Commands;

namespace Identity.Domain.CommandHandlers
{
    public interface  IPasswordResetCommandHandler
    {
        void Handle(PasswordResetCommand cmd);
    }
}
