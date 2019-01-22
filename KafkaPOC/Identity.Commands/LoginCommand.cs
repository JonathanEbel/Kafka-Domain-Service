using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Commands
{
    public class LoginCommand : CommandBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
