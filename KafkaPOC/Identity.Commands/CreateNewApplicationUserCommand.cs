using Core;
using System.Collections.Generic;

namespace Identity.Commands
{
    public class CreateNewApplicationUserCommand : CommandBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<string> Roles { get; set; }
    }
}
