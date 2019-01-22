using Core;
using System;

namespace Identity.Events
{
    public class UserLoggedInEvent : EventBase
    {
        public string UserName { get; set; }
    }
}
