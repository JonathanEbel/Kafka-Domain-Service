using System;

namespace Projections.Domain.Models
{
    public class Usage
    {
        public int ID { get; private set; }
        public Guid IdentityUserId { get; private set; }
        public string Username { get; private set; }
        public DateTime LastLogin { get; private set; }
        public DateTime LastBid { get; private set; }
        public string FullName { get; private set; }
        public string OrgName { get; private set; }
        public Guid UserId { get; private set; }
        public Guid OrgId { get; private set; }


        public Usage()
        { }

        public Usage(string username, string fullName, string orgName, Guid userId, Guid orgId)
        {
            Username = username;
            FullName = fullName;
            OrgName = orgName;
            UserId = userId;
            OrgId = orgId;
        }

        public void UpdateLastLogin(DateTime lastLoginDateTime)
        {
            LastLogin = lastLoginDateTime;
        }

    }
}
