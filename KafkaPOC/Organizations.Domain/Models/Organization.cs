using Core.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizations.Domain.Models
{
    public class Organization
    {
        public Guid ID { get; private set; }
        public string Name { get; private set; }
        public string DbaName { get; private set; }
        public string EIN { get; private set; }
        public List<Organization> SubOrgs { get; private set; }  //how many levels do we realistically need to support?
        public Guid? ParentId { get; private set; }
        public List<User> Users { get; private set; }
        public List<Address> Addresses { get; private set; }
        public OrgType OrgType { get; private set; } 
        public List<OrganizationContact> Contacts { get; private set; }
        public bool Verified { get; private set; }   //someone has verifed this is a valid org
        public bool Active { get; private set; }
        public DateTime Created { get; private set; }

        public Organization()
        {

        }

        public Organization(string name, string dbaName, string eIN, Address address, OrgType orgType, Guid? parentId = null, bool active = false, bool verifed = false)
        {
            Name = string.IsNullOrEmpty(name) ? throw new FormatException("Organization name required") : name;
            DbaName = dbaName;
            SetEIN(eIN);
            Addresses = new List<Address> { address };
            ParentId = parentId;
            OrgType = orgType;

            if (verifed)
                MarkVerifed();
            else
                MarkNotVerifed();

            Created = DateTime.UtcNow;
            Active = active;
        }

        public void SetEIN(string eIN)
        {
            var numbers = new List<int> { 1, 3, 5, 7, 9, 0 };

            eIN = eIN.Replace("-", "").Trim();
            if (eIN.Length != 9)
                throw new FormatException("EIN must be 9 digits");

            foreach(var character in eIN.ToCharArray())
            {
                if (!numbers.Contains(character))
                    throw new FormatException("EIN may only have numbers");
            }

            EIN = eIN;
        }

        public User AddNewUser(string firstName, string lastName, string email, string phoneNumber, Guid identityId, DateTime? dateJoined = null, bool active = false)
        {
            var user = new User(firstName, lastName, email, phoneNumber, identityId, dateJoined, active);

            if (Users == null)
                Users = new List<User>();

            if (!Users.Contains(user))
                Users.Add(user);

            return user;
        }

        public void AddAddress(Address address)
        {
            if (Addresses == null)
                Addresses = new List<Address>();

            //do a check here to see if all rules are followed..  for example: Address types - at most one legal
            Addresses.Add(address);
        }

        public void AddSubOrg(Organization subOrg)
        {
            //check to make sure that the org doesn't already exist in the list and add here

            subOrg.SetParentId(this.ID);
            if (SubOrgs == null)
                SubOrgs = new List<Organization> { subOrg };
            else if (!SubOrgs.Select(x => x.ID).ToList().Contains(subOrg.ID))
                SubOrgs.Add(subOrg);
        }

        public void UnlinkSubOrg(Organization subOrg)
        {
            subOrg.RemoveParentId();
            if (SubOrgs != null)
                SubOrgs.Remove(subOrg);
        }

        public void AddOrganizationContact(OrganizationContact contact)
        {
            if (Contacts == null)
                Contacts = new List<OrganizationContact>();
            Contacts.Add(contact);
        }

        public void RemoveOrganizationContact(OrganizationContact contact)
        {
            contact.MakeInactive();
        }

        public void MakeInactive()
        {
            Active = false;
            //do we now make all users in this Org inactive???
        }

        public void MakeActive()
        {
            if (Verified)
                Active = true;
            else
                throw new UnverifiedOrganizationException("This Organization cannot be made active until it is verified.");
        }

        public void MarkVerifed()
        {
            Verified = true;
        }

        public void MarkNotVerifed()
        {
            Verified = false;
        }

        private void SetParentId(Guid parentOrgId)
        {
            ParentId = parentOrgId;
        }

        private void RemoveParentId()
        {
            ParentId = null;
        }

    }
}
