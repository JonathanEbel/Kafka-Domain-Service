using System;
using System.Collections.Generic;

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
        public List<OrgType> OrgTypes { get; private set; }  //can an org actually be many types??
        public bool CanSell { get; private set; }
        public List<OrganizationContact> Contacts { get; private set; }
        public bool Active { get; private set; }

        public Organization()
        {

        }

        public Organization(string name, string dbaName, string eIN, Address address, bool canSell, Guid? parentId = null, bool active = false)
        {
            Name = string.IsNullOrEmpty(name) ? throw new Exception("Organization name required") : name;
            DbaName = dbaName;
            SetEIN(eIN);
            Addresses = new List<Address> { address };
            CanSell = canSell;
            ParentId = parentId;
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

        public void AddAddress(Address address)
        {
            if (Addresses == null)
                Addresses = new List<Address>();

            //do a check to see if all rules are followed..  for example: Address types - at most one legal
            Addresses.Add(address);
        }

        public void AddSubOrg(Organization organization)
        {
            //check to make sure that the org doesn't already exist in the list and add
            if (SubOrgs == null)
                SubOrgs = new List<Organization> { organization };
            else if (!SubOrgs.Contains(organization))
                SubOrgs.Add(organization);
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
            Active = true;
        }
    }
}
