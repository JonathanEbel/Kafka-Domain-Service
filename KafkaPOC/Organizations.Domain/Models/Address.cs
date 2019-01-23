using System;

namespace Organizations.Domain.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public StateProvince AddressStateProvince {get; private set; }
  //      public AddressType TypeOfAddress { get; private set; }

        public Address()
        {

        }

        public Address(string address1, string address2, string city, StateProvince state)
        {
            Address1 = string.IsNullOrEmpty(address1) ? throw new FormatException("Address1 cannot be empty.") : address1;
            Address2 = address2;
            City = string.IsNullOrEmpty(city) ? throw new FormatException("City cannot be empty.") : city;
            AddressStateProvince = state == null ? throw new Exception("State/Province is required.") : state;
        }

    }
}
