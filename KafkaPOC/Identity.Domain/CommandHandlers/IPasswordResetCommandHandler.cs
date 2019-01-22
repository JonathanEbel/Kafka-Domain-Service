using Identity.Commands;
using System;

namespace Identity.Domain.CommandHandlers
{
    public interface  IPasswordResetCommandHandler : IDisposable
    {
        void Handle(PasswordResetCommand cmd, bool useStrongPassword);
    }
}
