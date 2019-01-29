using Core;
using System;

namespace Organizations.Commands
{
    public class CreateNewUserCommand : CommandBase
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
