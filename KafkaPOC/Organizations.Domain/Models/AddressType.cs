using System;

namespace Organizations.Domain.Models
{
    public class AddressType
    {
        public enum AddressEnum
        {
            physical, pickup, legal
        }

        public static string GetFriendlyName(AddressEnum addressEnum)
        {
            return addressEnum.ToString();
        }

        //public int ID { get; private set; }
        //public string Name { get; private set; }
        //public string Description { get; private set; }
        //public bool Deleted { get; private set; }  //in theory, with event streaming, you don't really need soft deletion anymore, right?

        //public AddressType ()
        //{

        //}

        //public AddressType(string name, string description)
        //{
        //    Name = string.IsNullOrEmpty(name) ? throw new Exception("Name is required for the new Address type.") : name;
        //    Deleted = false;
        //}

        //public void MarkDeleted()
        //{
        //    Deleted = true;
        //}
    }
}
