using Core;
using System;
using System.Collections.Generic;

namespace Identity.Events
{
    public class ApplicationUserCreatedEvent : EventBase
    {
        public string UserName { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set; }
        public List<string> Claims { get; set; }
        public bool Active { get; set; }
    }
}
