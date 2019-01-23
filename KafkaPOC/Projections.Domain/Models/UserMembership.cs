using System;
using System.Collections.Generic;

namespace Projections.Domain.Models
{
    public class UserMembership
    {
        public int ID { get; private set; }
        public string UserFullName { get; private set; }
        public Guid UserId { get; private set;}
        public List<Guid> OrganizationMemberships { get; private set; }

        public UserMembership()
        {

        }

        public UserMembership(string userFullName, Guid userId, Guid orgId)
        {
            
        }
    }
}
