using System;
using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.Cryptography;
using Core.CustomExceptions;
using Core.Validation;

namespace Identity.Models
{
    public class ApplicationUser
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public DateTime LastLogin { get; private set; }
        public DateTime DateCreated { get; private set; }
        public List<ApplicationUserClaim> Claims { get; private set; }
        public bool Active { get; private set; }
        public int? FailedLoginAttempts { get; private set; }
        public DateTime LockedOutUntil { get; private set; }


        public ApplicationUser()
        { }


        public ApplicationUser(string userName, string password, string confirmPassword, bool requireStrongPassword, bool active = false)
        {
            if (confirmPassword != password)
                throw new ArgumentException("Password and Confirm Password parameters don't match");

            ConfirmPasswordStrength(password, requireStrongPassword);

            SetEmailAddressUserName(userName);
            Id = Guid.NewGuid();

            SetSaltAndPassword(password);

            DateCreated = DateTime.Now;

            Active = active;
        }


        public void UpdateLastLogin()
        {
            LastLogin = DateTime.Now;
            FailedLoginAttempts = 0;
        }


        public void AddClaim(string key, string value)
        {
            if (Claims == null)
                Claims = new List<ApplicationUserClaim>();

            Claims.Add(new ApplicationUserClaim
            {
                claimKey = key,
                claimValue = value
            });
        }


        public void AddIdentifyingClaim(Guid value)
        {
            AddClaim(Constants.IDENTIFYING_CLAIM, value.ToString());
        }


        public void ClearClaims()
        {
            if (Claims != null)
                Claims.Clear();
        }


        public void OverwriteUserName(string userName)
        {
            SetEmailAddressUserName(userName);
        }


        private void SetEmailAddressUserName(string userName)
        {
            if (Validation.IsValidEmail(userName))
                UserName = userName;
            else
                throw new FormatException("Email/Username formatted incorrectly.");

        }


        private void SetSaltAndPassword(string password)
        {
            //one way hash the password and store both the Salt and the hashed password
            Salt = Crypto.getSalt();
            Password = Crypto.getHash(password + Salt);
        }


        public void UpdatePassword(string newPassword, string confirmNewPassword, string oldPassword)
        {
            if (Crypto.getHash(oldPassword + Salt) != Password)
                throw new BadPasswordException("Old Password is incorrect");

            if (confirmNewPassword != newPassword)
                throw new ArgumentException("Password and Confirm Password parameters don't match");

            SetSaltAndPassword(newPassword);
        }


        public void MakeActive()
        {
            Active = true;
        }


        public void MakeInactive()
        {
            Active = false;
        }
        

        public bool Lockedout()
        {
            return (LockedOutUntil > DateTime.Now);
        }


        public bool Authenticates(string password, out string reason)
        {
            reason = "";

            if (Lockedout())
            {
                reason = "User is locked-out due to too many failed login attempts. Try again in a little while.";
                return false;
            }

            //if the user is not locked out, check the password...
            if (Crypto.getHash(password + Salt) == Password)
                return true;

            //user must have wrong password
            reason = "Username and password don't match";
            IncrementLoginFailure();
            return false;
        }


        private void IncrementLoginFailure()
        {
            //if this is the third time or more in a row that the user has failed login
            //lock this user out for X seconds
            if ((FailedLoginAttempts != null) && ((FailedLoginAttempts + 1) > 3))
            {
                LockedOutUntil = DateTime.Now.AddSeconds(75);
            }
            else if (FailedLoginAttempts == null)
            {
                FailedLoginAttempts = 0;
            }

            FailedLoginAttempts++;  //increment failed login attempts...
        }


        private void ConfirmPasswordStrength(string password, bool requireStrongPassword)
        {
            if (requireStrongPassword)
            {
                HashSet<char> specialCharacters = new HashSet<char>() { '%', '$', '#', '!', '@', '^', '&', '*', '+', '=' };
                if (!password.Any(char.IsLower) || 
                     !password.Any(char.IsUpper) ||
                     !password.Any(char.IsDigit) ||
                     password.Length < 4 ||
                     !password.Any(specialCharacters.Contains))
                {
                    throw new WeakPasswordException("Password must contain lower-case and upper-case letters, at least one digit and at least one special character.");
                }
            }

        }

    }

   
}
