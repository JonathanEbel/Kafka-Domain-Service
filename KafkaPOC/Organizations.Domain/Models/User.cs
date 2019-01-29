using System;
using Core.Validation;

namespace Organizations.Domain.Models
{
    public class User
    {
        public Guid ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateJoined { get; private set; }
        public Organization Organization { get; private set; }
//        public List<OrgRole> OrgRoles { get; private set; }  //this would be like admin, rep, etc...
        public bool Active { get; private set; }

        public User()
        {

        }

        public User(string firstName, string lastName, string email, string phoneNumber, DateTime? dateJoined = null, bool active = false)
        {
            FirstName = string.IsNullOrEmpty(firstName) ? throw new FormatException("First name is required") : firstName;
            LastName = string.IsNullOrEmpty(LastName) ? throw new FormatException("Last name is required") : lastName;
            SetEmailAddress(email);
            DateJoined = dateJoined == null ? DateTime.UtcNow : (DateTime)dateJoined;
            Active = active;
            PhoneNumber = phoneNumber;
        }

        public void SetEmailAddress(string email)
        {
            Email = Validation.IsValidEmail(email) ? email : throw new FormatException("Email address provided is not valid.");
        }

        public void MakeInactive()
        {
            Active = false;
        }

        public void MakeActive()
        {
            Active = true;
        }
    }
}
