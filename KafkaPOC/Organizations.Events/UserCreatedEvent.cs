using Core;
using System;

namespace Organizations.Events
{
    public class UserCreatedEvent : EventBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateJoined { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid IdentityId { get; set; }
        public bool Active { get; set; }
    }
}
