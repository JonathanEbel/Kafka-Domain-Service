using Core;
using System;

namespace Identity.Commands
{
    public class PasswordResetCommand : CommandBase
    {
        public Guid ApplicationUserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
