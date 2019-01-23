using Core.Validation;
using System;

namespace Organizations.Domain.Models
{
    public class OrganizationContact
    {
        public int ID { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ContactRole { get; private set; }
        public bool IsActiveContact { get; private set; }

        public OrganizationContact()
        {

        }

        public OrganizationContact(string fullName, string email, string phoneNumber, string contactRole)
        {
            if (string.IsNullOrEmpty(fullName) && string.IsNullOrEmpty(email))
                throw new Exception("At least one of Name or Email must be present.");

            SetEmail(email);
            IsActiveContact = true;
            FullName = fullName;
            PhoneNumber = phoneNumber; //do we want validation on this as well??
            ContactRole = contactRole;
        }

        public void SetEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                Email = Validation.IsValidEmail(email) ? email : throw new FormatException("Email address provided is not valid.");
        }

        public void MakeInactive()
        {
            IsActiveContact = false;
        }

        public void MakeActive()
        {
            IsActiveContact = true;
        }
    }

    
}
